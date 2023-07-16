using UserFriendlyUrlUsingGuid;

Guid TestIdAsGuid = Guid.Parse("9818a374-5a23-4c7d-b427-492ca7374ff9");
string TestIdAsString = "dKMYmCNafUy0J0kspzdP_Q";

string idBase64String = Convert.ToBase64String(TestIdAsGuid.ToByteArray());

Console.WriteLine(idBase64String);

string idString = Guider.ToStringFromGuid(TestIdAsGuid);

Console.WriteLine(idString);

var guid = Guider.ToGuidFromString(TestIdAsString);

Console.WriteLine(guid);
Console.ReadLine();