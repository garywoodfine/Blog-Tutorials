using System;

namespace Threenine.Activities.Addresses.Queries.Get;

public class Response
{
   public Address[] Addresses { get; set; }
}

public class Address
{
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string Postcode { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Property { get; set; }
    public string Organisation { get; set; }
}