using Contracts;
using Course.BLL.Responses;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;

namespace CourseAPI.Utility
{
    public class CourseLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<CourseDTO> _dataShaper;
        public CourseLinks(LinkGenerator linkGenerator, IDataShaper<CourseDTO>
       dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<CourseDTO> courseDTO, string fields,
HttpContext httpContext)
        {
            var shapedEmployees = ShapeData(courseDTO, fields);

            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkdedCourse(courseDTO, fields, httpContext, shapedEmployees);

            return ReturnShapedCourse(shapedEmployees);
        }

        private LinkResponse ReturnShapedCourse(List<Entity> shapedEmployees) =>
    new LinkResponse { ShapedEntities = shapedEmployees };

        private LinkResponse ReturnLinkdedCourse(IEnumerable<CourseDTO> courseDTO,
string fields, HttpContext httpContext, List<Entity> shapedEmployees)
        {
            var employeeDtoList = courseDTO.ToList();
            for (var index = 0; index < employeeDtoList.Count(); index++)
            {
                var employeeLinks = CreateLinksForCourse(httpContext,
               employeeDtoList[index].Id, fields);
                shapedEmployees[index].Add("Links", employeeLinks);
            }
            var employeeCollection = new LinkCollectionWrapper<Entity>(shapedEmployees);
            var linkedEmployees = CreateLinksForCourse(httpContext, employeeCollection);
            return new LinkResponse { HasLinks = true, LinkedEntities = linkedEmployees };
        }

        private List<Link> CreateLinksForCourse(HttpContext httpContext, Guid id, string fields = "")
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(httpContext, "Get", values: new {  id, fields }),
                "self",
                "GET"),
                //new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteEmployeeForCompany", values: new { id }),
                //"delete_employee",
                //"DELETE"),
                //new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateEmployeeForCompany", values: new { id }),
                //"update_employee",
                //"PUT"),
                //new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateEmployeeForCompany", values: new {  id }),
                //"partially_update_employee",
                //"PATCH")
            };
            return links;
        }


        private LinkCollectionWrapper<Entity> CreateLinksForCourse(HttpContext httpContext,
LinkCollectionWrapper<Entity> employeesWrapper)
        {
            employeesWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext,
           "Get", values: new { }),
            "self",
            "GET"));
            return employeesWrapper;
        }


        private List<Entity> ShapeData(IEnumerable<CourseDTO> courseDTO, string fields)
=>
            _dataShaper.ShapeData(courseDTO, fields)
                       .Select(e => e.Entity)
                       .ToList();
        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
