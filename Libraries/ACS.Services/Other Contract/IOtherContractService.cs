//create by saddam 
//date : 14/06/2016
//purpose : Insert, Update, Delete Records for Other Contract Master
using ACS.Core.Domain.Master;
using ACS.Core.Domain.OtherContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ACS.Services.Other_Contract
{
    public partial interface   IOtherContractService
    {
        int InsertOtherContract(OtherContractMaster OtherContract);

        void InsertOtherContractDocumentsLinking(OtherContractDocuments OtherContractDocumentsLink);

        void InsertOtherContractImageBank(OtherContractImageBank OtherContractImageBank);


        OtherContractMaster GetOtherContractMasterById(OtherContractMaster Author);

        int InsertOtherContractLink(OtherContractLink OtherContractLink);

        void InsertOtherContractDocumentsLinkingLink(OtherContractLinkDocument OtherContractLinkDocument);

        OtherContractMaster GetOtherContractMasterId(int Id);
        void UpdateOtherContractMaster(OtherContractMaster OtherContract);

        void UpdateOtherContractImageBank(OtherContractImageBank OtherContractImageBank);

        OtherContractDocuments getOtherContractDocumentsDetail(int DocumentId);

        void DeavtivateOtherContractDocumentsById(int id, int enteredBy);

        void UpdateOtherContractLink(OtherContractLink OtherContractLink);

        void DeavtivateOtherContractDocumentsLinkById(int id, int enteredBy);

        void InsertSearchHistory(OtherContractSearch _SearchParam);
        void InsertOtherContractDivisionLink(OtherContractDivisionLink DivisionLink);
        void DeleteOtherContractDivisionLink(int LinkingId, int enteredBy);
        void DeleteVideoImageBankLink(int LinkingId, int enteredBy);

        void InsertVideoImageBankLink(VideoImageBank VideoImageBank);
        void UpdateVideoImageBankLink(VideoImageBank VideoImageBank);
        
        void DeleteOtherContractMaster(OtherContractMaster OtherContract);

    }
}
