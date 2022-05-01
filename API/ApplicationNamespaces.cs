global using Microsoft.Extensions.Options;

global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;

global using raiding.API.models;
global using raiding.API.configuration;
global using raiding.API.services;
global using raiding.API.repositories;
global using raiding.API.context;
global using raiding.API.validators;
global using raiding.API.GraphQL.Mutation;
global using raiding.API.GraphQL.queries;

global using FluentAssertions;
global using FluentValidation;
global using FluentValidation.AspNetCore;