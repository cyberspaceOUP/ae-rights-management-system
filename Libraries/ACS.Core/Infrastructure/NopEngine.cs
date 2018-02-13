using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using ACS.Core.Configuration;
using ACS.Core.Infrastructure.DependencyRegister;

using System.Web.Http.Dependencies;

namespace ACS.Core.Infrastructure
{
    public class NopEngine : IEngine
    {
        #region Fields

        private ContainerManager _containerManager;

        #endregion

        #region Utilities

        /// <summary>
        /// Run startup tasks
        /// </summary>
        protected virtual void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            //sort
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        /// <summary>
        /// Register dependencies
        /// </summary>
        /// <param name="config">Config</param>
        protected virtual void RegisterDependencies(NopConfig config)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            //we create new instance of ContainerBuilder
            //because Build() or Update() method can only be called once on a ContainerBuilder.


            //dependencies
            var typeFinder = new WebAppTypeFinder(config);
            builder = new ContainerBuilder();
            builder.RegisterInstance(config).As<NopConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(container);

            //register dependencies provided by other assemblies
            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegister>();
            var drInstances = new List<IDependencyRegister>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegister)Activator.CreateInstance(drType));
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder);
            builder.Update(container);


            this._containerManager = new ContainerManager(container);

            //set dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //Added by Rahul Kumar on 25/02/2016 to include DI for WebAPI
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(container);
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize components and plugins in the nop environment.
        /// </summary>
        /// <param name="config">Config</param>
        public void Initialize(NopConfig config)
        {
            //register dependencies
            RegisterDependencies(config);

            //startup tasks
            if (!config.IgnoreStartupTasks)
            {
                RunStartupTasks();
            }

        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Container manager
        /// </summary>
        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        #endregion
    }
}
