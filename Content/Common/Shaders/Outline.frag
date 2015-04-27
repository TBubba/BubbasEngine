uniform sampler2D texture;
uniform float textureAlpha;
uniform vec2 stepSize;
uniform vec4 outlineColor;

int SampleAlpha(sampler2D tex, vec2 texturePos)
{
    vec4 textureCol = texture2D(tex, texturePos);

    if (textureCol.w > 0.0)
    	return 1;
   	return 0;
}

vec4 FragColor(sampler2D tex, vec2 texturePos)
{
    vec4 color = texture2D(texture, texturePos);

    if (color.w > 0.0)
    {
    	color.w = textureAlpha;
    	return color;
	}

	color = outlineColor;
	int alpha;

    alpha += SampleAlpha(texture, texturePos + vec2(stepSize.x, 0.0));
    alpha += SampleAlpha(texture, texturePos + vec2(-stepSize.x, 0.0));
    alpha += SampleAlpha(texture, texturePos + vec2(0.0, stepSize.y));
    alpha += SampleAlpha(texture, texturePos + vec2(0.0, -stepSize.y));

    alpha += SampleAlpha(texture, texturePos + vec2(stepSize.x, stepSize.y));
    alpha += SampleAlpha(texture, texturePos + vec2(stepSize.x, -stepSize.y));
    alpha += SampleAlpha(texture, texturePos + vec2(-stepSize.x, stepSize.y));
    alpha += SampleAlpha(texture, texturePos + vec2(-stepSize.x, -stepSize.y));

    if (alpha == 0)
    	color.w = 0.0;

    return color;
}
 
void main()
{
    vec4 color = FragColor(texture, gl_TexCoord[0].xy);
    gl_FragColor = color;
}