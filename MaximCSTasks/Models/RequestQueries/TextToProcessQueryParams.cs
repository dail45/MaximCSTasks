using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MaximCSTasks.Models.RequestQueries;

public record TextToProcessQueryParams(
    [BindRequired]
    string Line,
    [BindRequired]
    string SorterType
);