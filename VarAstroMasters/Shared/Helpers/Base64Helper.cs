using Microsoft.AspNetCore.Components.Forms;

namespace VarAstroMasters.Shared.Helpers;

public static class Base64Helper
{
    public static async Task<string> ImageToB64(IBrowserFile image)
    {
        var format = "image/png";
        // This might be modified but 500x500 is from the original system.
        var maxPixelsPerSide = 500;
        var resizedImage = await image.RequestImageFileAsync(format, maxPixelsPerSide, maxPixelsPerSide);
        var buffer = new byte[resizedImage.Size];
        await resizedImage.OpenReadStream().ReadAsync(buffer);
        return $"data:{format};base64,{Convert.ToBase64String(buffer)}";
    }
}