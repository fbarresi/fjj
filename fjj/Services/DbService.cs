
using System.IO;

public class DbService 
{
  
    private string GetSaveLocation()
		{
			var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
			return Path.GetDirectoryName(location);
		}
  
}
