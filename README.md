# NhOdataTest

Testing [NHibernate](https://github.com/nhibernate/nhibernate-core), [Automapper](https://github.com/AutoMapper/AutoMapper) and [Odata](https://github.com/OData/WebApi) together

List of tests:
- OK `https://localhost:5001/odata/weatherforecast`
- OK `https://localhost:5001/odata/weatherforecast?$orderby=temperatureF`
- OK `https://localhost:5001/odata/weatherforecast?$filter=summary eq 'Warm'`
- ERROR `https://localhost:5001/odata/weatherforecast?$select=id,summary` (Fixed: [#2079](https://github.com/nhibernate/nhibernate-core/pull/2079))
- ERROR `https://localhost:5001/odata/weatherforecast?$apply=groupby((summary), aggregate(temperatureC with average as total))` (Fixed: [#2322](https://github.com/nhibernate/nhibernate-core/pull/2322))
- ERROR `https://localhost:5001/odata/weatherforecast?$filter=town eq 'London'` (Bug: [#2380](https://github.com/nhibernate/nhibernate-core/issues/2380))

Related issues: 
- https://github.com/nhibernate/nhibernate-core/issues/2334
- https://github.com/nhibernate/nhibernate-core/issues/2380
- https://github.com/OData/WebApi/issues/2015
- https://github.com/DataObjects-NET/dataobjects-net/issues/13


EntityFrameworkCore test: https://github.com/fairking/EfOdataTest

NHibernate test: https://github.com/fairking/NhOdataTest

DataObjects.NET test: https://github.com/fairking/DoOdataTest
