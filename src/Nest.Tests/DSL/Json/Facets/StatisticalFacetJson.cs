﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Facets
{
  [TestFixture]
  public class StatisticalFacetJson
  {
    [Test]
    public void TestStatisticalFacet()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .Query(@"{ raw : ""query""}")
        .FacetStatistical(sf=>sf
          .OnField(f=>f.LOC)
        );

      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""loc.sort"" :  {
                statistical : {
                  field : ""loc.sort""
               } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void TestStatisticalFacetFields()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .Query(@"{ raw : ""query""}")
        .FacetStatistical("needs_a_name", sf => sf
          .OnFields(f => f.LOC, f=>f.LongValue)
        );

      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""needs_a_name"" :  {
                statistical : {
                  fields : [""loc.sort"", ""longValue""]
               } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void TestStatisticalFacetScript()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .Query(@"{ raw : ""query""}")
        .FacetStatistical("needs_a_name", sf => sf
          .Script("doc['num1'].value + doc['num2'].value")
        );

      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""needs_a_name"" :  {
                statistical : {
                  script : ""doc['num1'].value + doc['num2'].value""
               } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void TestStatisticalFacetScriptParams()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .Query(@"{ raw : ""query""}")
        .FacetStatistical("needs_a_name", sf => sf
          .Script("(doc['num1'].value + doc['num2'].value) * factor")
          .Params(p=>p.Add("factor", 5))
        );

      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""needs_a_name"" :  {
                statistical : {
                  script : ""(doc['num1'].value + doc['num2'].value) * factor"",
                  params : {factor:5}
               } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
  }
}
