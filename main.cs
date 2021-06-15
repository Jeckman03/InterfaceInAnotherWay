using System;
using System.Collections.Generic;

class MainClass {
  public static void Main (string[] args) {

		PersonEntry person = new PersonEntry
		{
			FirstNamne = "Jeff",
			LastName = "Eckman"
		};

		AddressEntry newAddress = new AddressEntry(person);
		newAddress.SaveRecord("1421 S Maple", "Mesa", "AZ", "85202");
		newAddress.SaveRecord("2207 S Noche De Paz", "Mesa", "AZ", "85206");

		int count = 1;

		foreach (var address in person.addresses)
		{
			Console.WriteLine($"{ count }: { address.DisplayAddress }");
			++count;
		}

    Console.ReadLine();
  }
}

// Interface
public interface ISaveAddress
{
	void SaveAddress(AddressModel address);
}

// Address Model
public class AddressModel : PersonModel
{
	public string Street { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string ZipCode { get; set; }

	public string DisplayAddress => $"{ Street }, { City }, { State }  { ZipCode }";
}

// Address Entry Form
public class AddressEntry : AddressModel
{
	ISaveAddress _parent;

	// A constructor that links PersonEntry class
	public AddressEntry(ISaveAddress parent)
	{
		_parent = parent;
	}

	public void SaveRecord(string street, string city, string state, string zipCode)
	{
		AddressModel address = new AddressModel
		{
			Street = street,
			City = city,
			State = state,
			ZipCode = zipCode
		};

		// The SaveAddress method can be called by using the ISaveAddress variable
		_parent.SaveAddress(address);
	}
}

// Person Model
public class PersonModel
{
	public string FirstNamne { get; set; }
	public string LastName { get; set; }
}

// Person Entry Form
public class PersonEntry : AddressModel, ISaveAddress
{
	// Creates a list of addresses
	public List<AddressModel> addresses = new List<AddressModel>();

	// This method is called in the Address class from the SaveRecord method
	// Which adds the address to the list
	public void SaveAddress(AddressModel address)
	{
		addresses.Add(address);
	}
}

