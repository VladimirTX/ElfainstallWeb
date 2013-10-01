using System.Xml.Serialization;

namespace ElfaInstall.Classes
{
    public class Order
    {
        [XmlElement]
        public StoreInfo Store;
        [XmlElement]
        public CustomerInfo Customer;
        [XmlElement]
        public InstallInfo Install;
        [XmlElement]
        public ChargesInfo Charges;
        [XmlElement]
        public OrderAttributes Attributes;
    }
    public class StoreInfo
    {
        [XmlElement]
        public string Name;
        [XmlElement]
        public string SaleDate;
        [XmlElement]
        public string OrderNumber;
        [XmlElement]
        public string Planner;
        [XmlElement]
        public string ChangeOrder;
    }

    public class CustomerInfo
    {
        [XmlElement]
        public string FirstName;
        [XmlElement]
        public string MiddleName;
        [XmlElement]
        public string LastName;
        [XmlElement]
        public string Address1;
        [XmlElement]
        public string Address2;
        [XmlElement]
        public string City;
        [XmlElement]
        public string State;
        [XmlElement]
        public string Zip;
        [XmlElement]
        public string HomePhone;
        [XmlElement]
        public string OtherPhone;
        [XmlElement]
        public string Email;
    }

    public class InstallInfo
    {
        [XmlElement]
        public string InstallDate;
        [XmlElement]
        public string InstallWindowStartDate;
        [XmlElement]
        public string InstallWindowStartTime;
        [XmlElement]
        public string InstallWindowEndDate;
        [XmlElement]
        public string InstallWindowEndTime;
        [XmlElement]
        public string DeliveryOption80;
        [XmlElement]
        public string FulfillmentType;
        [XmlElement]
        public string OutsideDeliveryDate;
        [XmlElement]
        public string OutsideDeliveryWindowStart;
        [XmlElement]
        public string CustomerPickupDate;
        [XmlElement]
        public string CustomerPickupAppointment;
        [XmlElement]
        public string Demolition;
        [XmlElement]
        public string ScopeOfDemo;
        [XmlElement]
        public string SolutionDescription;
        [XmlElement]
        public string AdditionalComments;
    }

    public class ChargesInfo
    {
        [XmlElement]
        public decimal RetailPrice;
        [XmlElement]
        public decimal AdditionalDemo;
        [XmlElement]
        public decimal Total;
    }

    public class OrderAttributes
    {
        [XmlElement]
        public OneAttribute[] Attribute;
    }

    public class OneAttribute
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string value;
    }
}