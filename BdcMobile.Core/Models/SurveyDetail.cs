using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.Models
{    
    public class SurveyDetail
    {
        public int SurveyID { get; set; }
        public string SurveyNo { get; set; }
        public string DepartmentID { get; set; }
        public string PartnerRepID_Requester { get; set; }
        public string TOR { get; set; }
        public string SOR { get; set; }
        public string LetterArrivalDate { get; set; }
        public string LetterNo { get; set; }
        public string PartnerRepID_PIC { get; set; }
        public string SurveyDescription { get; set; }
        public string SurveyGroupID { get; set; }
        public string PlaceOfSurvey { get; set; }
        public string TimeOfSurvey { get; set; }
        public string SurveyStatus { get; set; }
        public string ReportFrequency { get; set; }
        public object EstimatedFee { get; set; }
        public object ActualFee { get; set; }
        public object TotalSurveyExpense { get; set; }
        public object SurveyClosingDate { get; set; }
        public string CreateTime { get; set; }
        public object VoyageName { get; set; }
        public object CargoName { get; set; }
        public object CargoVolume_cont { get; set; }
        public object CargoVolume_package { get; set; }
        public object BLNo { get; set; }
        public object LoadingPort { get; set; }
        public object DischargingPort { get; set; }
        public object VoyageDate { get; set; }
        public object PartnerID_CargoOwner { get; set; }
        public object PartnerID_CargoConsigner { get; set; }
        public object PartnerID_CargoConsignee { get; set; }
        public object PartnerID_VoyageOperator { get; set; }
        public string ShipID { get; set; }
        public object InsuranceNo { get; set; }
        public object PartnerID_Insurer { get; set; }
        public object PartnerID_Insured { get; set; }
        public object ReportPhotoTempFileIndex { get; set; }
        public object ETD { get; set; }
        public object ETA { get; set; }
        public object DateOfAccident { get; set; }
        public object PlaceOfAccident { get; set; }
        public string CompanyBranchName { get; set; }
        public string CountForSalary { get; set; }
        public object CalculatorEstimateFee { get; set; }
        public string FinanceApprove { get; set; }
        public object CurrentStage { get; set; }
        public string SurveyTypeForFinance { get; set; }
        public string ExchangeRate { get; set; }
        public string PartnerRepPIC_Phone { get; set; }
        public string Is_active { get; set; }
        public string PartnerName { get; set; }
        public string DisplayName { get; set; }
        public string Inspector { get; set; }
        public string PartnerID { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public string ShipName { get; set; }
        public string PNR_Name { get; set; }
        public string PartnerRep_Name { get; set; }
        public object SendingDate { get; set; }
        public string SurveyGroupName { get; set; }
    }

    public class SurveyDetailResponse
    {
        public List<SurveyDetail> data { get; set; }
    }
}
