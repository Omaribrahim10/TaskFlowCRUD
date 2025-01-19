namespace Company.RouteMVC3.PL.Services
{
	public interface ISingltonService
	{
		public Guid Guid { get; set; }

		string GetGuid();
	}
}
