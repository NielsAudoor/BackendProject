namespace raiding.API.GraphQL.Mutation;

public record AddDPSInput(string CharacterName, int ILevel, int DamagePerSecond);