namespace raiding.API.GraphQL.Mutation;

public record AddHealerInput(string CharacterName, int ILevel, int HealingPerSecond);