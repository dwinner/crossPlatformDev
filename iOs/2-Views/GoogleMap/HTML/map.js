function displayMap (mapDivId, latInputId, lngInputId) {
   var lat = document.getElementById (latInputId).value;
   var lng = document.getElementById (lngInputId).value;

   const mapCenter = new window.google.maps.LatLng (lat, lng);

   const options = {
      zoom: 14,
      center: mapCenter
   };

// ReSharper disable once ConstructorCallNotUsed
   new window.google.maps.Map (document.getElementById (mapDivId), options);
}

function displayGeocoordinate (address) {
// ReSharper disable once UseOfImplicitGlobalInFunctionScope
   const geocoder = new google.maps.Geocoder();

   geocoder.geocode ({ 'address': address },
      function (results, status) {
         if (status === "OK") {
            const geocoordinate = results[0].geometry.location;

            alert (
               `Address: ${address}\nLat: ${geocoordinate.lat().toFixed (3)}\nLng: ${geocoordinate.lng().toFixed (3)}`);
         } else {
            alert (`Geocoder failed: ${status}`);
         }
      });
}