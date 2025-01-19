namespace Company.RouteMVC3.PL.Services
{
	public class SingltonService : ISingltonService
	{
		public Guid Guid { get; set; }
		public SingltonService()
		{
			Guid = Guid.NewGuid();
		}
		public string GetGuid()
		{
			return Guid.ToString();
		}
	}
}
