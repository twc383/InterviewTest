
#FundsLibrary Interview Test

Here is a simple 2 tier application containing an MVC web front-end and a WebApi service. Currently a simple overview page for Fund Managers and a details page are available.

####Possible improvements (please pick 1)
* Implement an add fund manager method end to end including use of the task based asynchronous pattern and MVC validation.
* Implement an edit screen using async and MVC validation.
* Implement logging
* Implement paging/sorting
* Implement search
* Create a MVVM client side component to represent a read only view of a fund manager. 
Include a calculated property for YearsManaged which can be computed from DateTime.Now - ManagedSince. 
This may replace the detail view. (You are free to use a higher level language which can compile to javascript, 
and any of the major javascript binding frameworks such as Knockout or Angular)
* Set up some form of basic authentication
* Write an external application which polls the service periodically and copies all new fund managers not already copied to a file.

####Minor improvements (please pick 1)
* Improve look and feel with the aid of bootstrap
* Implement bundling
* Add not-found page when attempting to view a non-existent Id
* Set up output caching for both tiers. Ensure appropriate caches are invalidated when necessary.
* Add a new property to the model of any type you wish. Ensure that the application remains backwards compatible.
* Refactor css to use a preprocessor (SASS or LESS)