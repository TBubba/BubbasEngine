uniform float time;
uniform vec2 resolution;
uniform vec4 bubbleColor;
uniform vec4 backColor;
uniform float bubbleSize;
uniform float bubbleFade;

void main()
{
    vec2 p = ( gl_FragCoord.xy / resolution.xy );
    float pi = 3.141592653589793;
    float theta = pi / 4.0;
    #define time time + atan(p.x, p.y)*2.0
    mat2 m = mat2(cos(theta), -sin(theta), sin(theta), cos(theta));
    p = p * 2.0 - 1.0;
    p.x *= resolution.x / resolution.y;
    p = m * p;
    vec2 f = fract(p * bubbleSize);
    f = 2.0 * f - 1.0;
    
    float df = distance(f, vec2(0.0, 0.0));
    df = 3.0*df*df - 2.0*df*df*df;
    float dp = max(1.5 - distance(p, vec2(0.0, 0.0)), 0.0);
    dp = 3.0*dp*dp - 2.0*dp*df*df;
    float from = 0.3 + sin(dp * pi * 0.5 + time * 0.5) * 0.75;
    float to = from + bubbleFade;
    float d = smoothstep(from, to, df);

    vec4 color = bubbleColor * (1.0 - d) + backColor * d;
    gl_FragColor = color;
}