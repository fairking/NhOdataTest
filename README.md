# NhOdataTest

Testing NHibernate, Automapper and Odata together

List of tests:
The url https://localhost:5001/odata/weatherforecast gives data;
The url https://localhost:5001/odata/weatherforecast?$orderby=temperatureF gives data;
The url https://localhost:5001/odata/weatherforecast?$filter=summary eq 'Warm' gives data;
The url https://localhost:5001/odata/weatherforecast?$select=id,summary throws an error;
The url https://localhost:5001/odata/weatherforecast?$apply=groupby((summary), aggregate(temperatureC with average as total)) throws an error;

Related issue: https://github.com/nhibernate/nhibernate-core/issues/2334
