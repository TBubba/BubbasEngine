uniform sampler2D texture;
uniform vec2 center;
uniform float radius;
uniform float innerRadius;

void main()
{
    vec4 color = texture2D(texture, gl_TexCoord[0].xy);

    vec2 delta = gl_TexCoord[0].xy - center;
    float dist = sqrt(delta * delta);

    if (dist > radius)
    {
        color.w = 0.1;
    }

    if (dist <= innerRadius)
    {
        color.w = 1.0;
    }

    if (dist <= 0.5)
    {
        color.w = (dist - innerRadius) / (dist - radius);
    }

    gl_FragColor = color;
}