//Added By Suranjana on 13/07/2016

using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace ACS.Services.Master
{
    public partial interface IManuscriptDeliveryFormatService
    {
        /// <summary>
        /// Method to get all ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <returns>returns list of ManuscriptDeliveryFormatMaster</returns>
        IList<ManuscriptDeliveryFormatMaster> GetAllManuscriptDeliveryFormat();

        /// <summary> 
        /// Method to check duplicate value
        /// </summary>
        /// <param name="ManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as parameter</param>
        /// <returns>returns string</returns>
        String DuplicateCheck(ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormat);

        /// <summary>
        /// Methos to insert ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <param name="ManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as parameter</param>
        void InsertManuscriptDeliveryFormat(ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormat);

        /// <summary>
        /// Methos to Fetch ManuscriptDeliveryFormatMaster Data
        /// </summary>
        /// <param name="ManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as parameter</param>
        /// <returns>returns ManuscriptDeliveryFormatMaster object</returns>
        ManuscriptDeliveryFormatMaster GetManuscriptDeliveryFormatById(ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormat);

        /// <summary>
        /// Methos to update ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <param name="ManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as parameter</param>
        void UpdateManuscriptDeliveryFormat(ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormat);

        /// <summary>
        /// Methos to delete ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <param name="ManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as parameter</param>
        void DeleteManuscriptDeliveryFormat(ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormat);
    }
}
