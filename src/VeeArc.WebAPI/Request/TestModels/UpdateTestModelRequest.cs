namespace VeeArc.WebAPI.Request.TestModels;

public class UpdateTestModelRequest
{
    public string Title { get; init; } = string.Empty;

    public string? Text { get; init; }

    public int? PageIndex { get; init; }
}
