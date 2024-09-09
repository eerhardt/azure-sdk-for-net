// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.ClientModel.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure.AI.OpenAI.Internal;

internal static class AdditionalPropertyHelpers
{
    internal static T GetAdditionalProperty<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T>(
        IDictionary<string, BinaryData> additionalProperties, string key)
        where T : class, IJsonModel<T>
    {
        if (additionalProperties?.TryGetValue(key, out BinaryData binaryProperty) != true)
        {
            return null;
        }
        return (T)ModelReaderWriter.Read(binaryProperty, typeof(T));
    }

    internal static IList<T> GetAdditionalListProperty<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T>(
        IDictionary<string, BinaryData> additionalProperties, string key)
        where T : class, IJsonModel<T>
    {
        if (additionalProperties?.TryGetValue(key, out BinaryData binaryProperty) != true)
        {
            return null;
        }
        List<T> items = [];
        using JsonDocument document = JsonDocument.Parse(binaryProperty);
        foreach (JsonElement element in document.RootElement.EnumerateArray())
        {
            items.Add((T)ModelReaderWriter.Read(
                BinaryData.FromObjectAsJson(element, AzureOpenAIJsonSerializerContext.Default.JsonElement),
                typeof(T)));
        }
        return items;
    }

    internal static void SetAdditionalProperty<T>(IDictionary<string, BinaryData> additionalProperties, string key, T value)
    {
        using MemoryStream stream = new();
        using (Utf8JsonWriter writer = new(stream))
        {
            writer.WriteObjectValue(value);
        }
        stream.Position = 0;
        BinaryData binaryValue = BinaryData.FromStream(stream);
        additionalProperties[key] = binaryValue;
    }
}

[JsonSerializable(typeof(JsonElement))]
internal partial class AzureOpenAIJsonSerializerContext : JsonSerializerContext { }
