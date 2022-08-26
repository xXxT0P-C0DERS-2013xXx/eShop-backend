// Global using directives

global using System.Net;
global using System.Reflection;
global using System.Text.Json;
global using System.Web.Http.Controllers;
global using System.Web.Http.Filters;
global using Catalog.API.Middlewares;
global using Catalog.Application.Common.Response.CustomResponses;
global using Catalog.BusinessLogic.Configuration;
global using Catalog.BusinessLogic.Contracts.Services;
global using Catalog.BusinessLogic.Models;
global using Catalog.BusinessLogic.Validators;
global using Catalog.Persistence;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using StackExchange.Redis;
global using MapperConfiguration = Catalog.BusinessLogic.Configuration.MapperConfiguration;