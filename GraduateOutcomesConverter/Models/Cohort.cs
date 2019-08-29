using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduateOutcomesConverter.Models
{

    public class Results
    {
        public Cohort CohortList { get; set; }
    }
    public class Cohort
    {
        public string HusId{ get; set; }
        public string OwnStu { get; set; }
        public string Country { get; set; }
        public string GoForenames { get; set; }
        public string GoSurname { get; set; }
        public string IsisForenames { get; set; }
        public string IsisSurname { get; set; }
        public string FNameChange { get; set; }
        public string SNameChange { get; set; }
        public string GradStatus { get; set; }
        public string UKMob1 { get; set; }
        public string UKMob2 { get; set; }
        public string UKTel1 { get; set; }
        public string UKTel2 { get; set; }
        public string IntTel1 { get; set; }
        public string IntTel2 { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string PersonUrn { get; set; }
        public string TblMasterFacultyCode { get; set; }
        public string PrimaryDept { get; set; }
        public string StudentNumber { get; set; }
        public string TblAddStuDetailsCode { get; set; }
        public string PrimaryTargetCode { get; set; }
        public string PrimaryTargetName { get; set; }
        public string IncomeStatusCode { get; set; }
        public string EnrolmentStatusCode { get; set; }
        public string MobileNumber { get; set; }
        public string PersonalEmail { get; set; }
        public string EmailAddress { get; set; }
        public string TypeCode { get; set; }
        public string TelephoneNumber { get; set; }
        public string HesaReasonForLeavingCode { get; set; }
        public DateTime DateOfWithdrawal { get; set; }
        public string FormattedTelNumber { get; set; }
        public string MatchMobileAndTelNo { get; set; }
    }

    public class Links
    {
        public string fileLocation { get; set; }
    }
}
