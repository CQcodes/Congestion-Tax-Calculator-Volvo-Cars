* Could not understand the purpose of having a Vehicle Interface and separate Vehicle Type classes since they were just giving back the vehicle type.

* That's why I have kept the 'vehicle' field in the Request object as a string.

* If we have list of all the possible vehicle type, we can make the field of type enum instead of a string.

* Making the tax calculation configurable based on different cities.
	* I have made two contracts for Tax-Calculation and Tax-Rules.
	* We can have different implementations for different cities.
	* Appropriate implementation can be injected based on the need.
	
* Making the tax calculation configurable
	* If we are going to have the same tax rules and calculation logic
	* and just the dependent values changes
	* then we can fetch it from the appSettings
	
* Using POST to get tax calculation
	* I could have used GET and pass the parameters as RouteData and Query Params
	* But we have Dates array. Which can be of any size. Which will result in huge URI.
	* Hence using POST, and passing the parameters in the request body
