using System;
using System.Windows.Forms;

namespace GymMasterAppDemo.Views
{
    public interface ICustomerEditView
    {
        string CustomerIdText { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string FatherName { get; set; }
        string Gender { get; set; }
        string OccupationId { get; set; }
        DateTime? Birthday { get; set; }
        string Address { get; set; }
        string City { get; set; }
        string Mobile { get; set; }
        string Home { get; set; }
        string Email { get; set; }
        DateTime CreationDate { get; set; }
        string Comments { get; set; }

        string FormTitle { set; }
        string SaveButtonText { set; }

        // Health Record: Ιατρικός φάκελος
        bool? HasBodyPain { get; set; }
        string BodyPainDesc { get; set; }

        bool? IsObest { get; set; }
        bool? IsSmoker { get; set; }
        bool? FamilyHeartHistory { get; set; }
        bool? HasHypertasis { get; set; }
        bool? HasDiabetes { get; set; }
        bool? HasHeartIssues { get; set; }
        bool? HasAsthma { get; set; }
        bool? HasThyroedes { get; set; }
        bool? HasArthritis { get; set; }
        bool? HasOsteoporosis { get; set; }
        bool? HasAllergies { get; set; }
        bool? HasMyosceletic { get; set; }
        string MyoskeleticDesc { get; set; }

        bool? HasOther { get; set; }
        string OtherDesc { get; set; }

        string EmergencyContact { get; set; }
        string EmergencyPhone { get; set; }
        string HealthComment { get; set; }

        void SetOccupationBindingSource(BindingSource source);
        void SetCustomerIdReadOnly(bool readOnly);
        void EnableHealthTab(bool enabled);
        void SelectHealthTab();

        void ShowMessage(string message);
        void ShowWarning(string message);
        DialogResult ShowQuestion(string message);

        void CloseView();

        event EventHandler LoadFormEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler CustomerIdLeaveEvent;
    }
}