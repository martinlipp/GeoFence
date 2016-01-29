using System;

using UIKit;
using CoreLocation;

namespace GeoFence
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			var LocationManager = new CLLocationManager();
			LocationManager.RequestWhenInUseAuthorization ();
			LocationManager.RequestAlwaysAuthorization ();

			CLCircularRegion region = new CLCircularRegion (new CLLocationCoordinate2D (+46.833120, +15.34901), 1000.0, "FF Gussendorf");

			if (CLLocationManager.LocationServicesEnabled) {

				LocationManager.DidStartMonitoringForRegion += (o, e) => {
					Console.WriteLine ("Now monitoring region {0}", e.Region.ToString ());
				};

				LocationManager.RegionEntered += (o, e) => {
					Console.WriteLine ("Just entered " + e.Region.ToString ());
				};

				LocationManager.RegionLeft += (o, e) => {
					Console.WriteLine ("Just left " + e.Region.ToString ());
				};

				LocationManager.Failed += (o, e) => {
					Console.WriteLine (e.Error);
				};

				LocationManager.UpdatedLocation += (o, e) => {
					Console.WriteLine ("Lat " + e.NewLocation.Coordinate.Latitude + ", Long " + e.NewLocation.Coordinate.Longitude);
				};

				LocationManager.LocationsUpdated += (o, e) => Console.WriteLine ("Location change received");

				LocationManager.StartMonitoring (region);
				LocationManager.StartMonitoringSignificantLocationChanges ();

				//LocationManager.StopMonitoringSignificantLocationChanges ();



			} else {
				Console.WriteLine ("This app requires region monitoring, which is unavailable on this device");
			}
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

