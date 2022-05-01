namespace raiding.API.GraphQL.Mutation;

public record AddRaidInput(string name, string description, Healer healer, Tank tank, DPS dps);