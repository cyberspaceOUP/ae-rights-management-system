//create by saddam 
//date : 01/07/2016
//purpose : Insert ISBN Bag
using ACS.Core.Domain.Master;
using ACS.Core.Domain.OtherContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ACS.Services.Master
{
   public partial interface  IISBNService
    {
       void InsertISBNBag(IList<ISBNBag> ISBNBagList);

       int GetIsbnByIsbn(string isbn);
       
    }
}
