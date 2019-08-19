using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService_BpmOnline_CRUD
{

    public class Id
    {
        public int dataValueType { get; set; }
    }

    public class Name
    {
        public int dataValueType { get; set; }
    }

    public class CreatedOn
    {
        public int dataValueType { get; set; }
    }

    public class Completeness
    {
        public int dataValueType { get; set; }
    }

    public class Zip
    {
        public int dataValueType { get; set; }
    }

    public class AccountType
    {
        public int dataValueType { get; set; }
    }

    public class AccountLogo
    {
        public int dataValueType { get; set; }
        public bool isLookup { get; set; }
        public string referenceSchemaName { get; set; }
    }

    public class RowConfig
    {
        public Id Id { get; set; }
        public Name Name { get; set; }
        public CreatedOn CreatedOn { get; set; }
        public Completeness Completeness { get; set; }
        public Zip Zip { get; set; }
        public AccountType AccountType { get; set; }
        public AccountLogo AccountLogo { get; set; }
    }

    public class Row
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Completeness { get; set; }
        public string Zip { get; set; }
        public string AccountType { get; set; }
        public object AccountLogo { get; set; }
    }

    public class RootObject
    {
        public RowConfig rowConfig { get; set; }
        public List<Row> rows { get; set; }
        public List<object> notFoundColumns { get; set; }
        public int rowsAffected { get; set; }
        public bool nextPrcElReady { get; set; }
        public bool success { get; set; }
    }
}
