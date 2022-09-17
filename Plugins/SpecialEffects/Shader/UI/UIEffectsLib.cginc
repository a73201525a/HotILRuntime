#ifndef UI_EFFECTS_LIB
#define UI_EFFECTS_LIB

//������� condition == 1������ trueValue����� condition == 0������ falseValue
half If(fixed condition, half trueValue, half falseValue)
{
	return trueValue * condition + falseValue * (1 - condition);
}

//������� condition == 1������ trueValue����� condition == 0������ falseValue
half2 If(fixed condition, half2 trueValue, half2 falseValue)
{
	return trueValue * condition + falseValue * (1 - condition);
}

//������� condition == 1������ trueValue����� condition == 0������ falseValue
half3 If(fixed condition, half3 trueValue, half3 falseValue)
{
	return trueValue * condition + falseValue * (1 - condition);
}

//������� condition == 1������ trueValue����� condition == 0������ falseValue
half4 If(fixed condition, half4 trueValue, half4 falseValue)
{
	return trueValue * condition + falseValue * (1 - condition);
}

//����һ����ɫ������
half GetBrightness(fixed3 color)
{
	return 0.299f * color.r + 0.587f * color.g + 0.114f * color.b;
}

//RGBɫ�ʿռ�ת����HSVɫ�ʿռ�
float3 RGBToHSV(float3 color)
{
	float4 k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	float4 p = lerp(float4(color.bg, k.wz), float4(color.gb, k.xy), step(color.b, color.g));
	float4 q = lerp(float4(p.xyw, color.r), float4(color.r, p.yzx), step(p.x, color.r));

	float d = q.x - min(q.w, q.y);
	float e = 1.0e-10;
	return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

//HSVɫ�ʿռ�ת����RGBɫ�ʿռ�
float3 HSVToRGB(float3 color)
{
	float3 rgb = clamp(abs(fmod(color.x * 6.0 + float3(0.0, 4.0, 2.0), 6) - 3.0) - 1.0, 0, 1);
	rgb = rgb * rgb * (3.0 - 2.0 * rgb);
	return color.z * lerp(float3(1, 1, 1), rgb, color.y);
}

//����ά����point2������Բ��center��˳ʱ����תradian����
float2 RotatePoint2(float2 point2, float2 center, half radian)
{
	half radius = distance(point2, center);
	half angle = atan((point2.y - center.y) / (point2.x - center.x)) - radian;
	point2.x = cos(angle) * radius + center.x;
	point2.y = sin(angle) * radius + center.y;
	return point2;
}

//��һ�����Ƿ���ָ�����������ڣ����򷵻�1�����򷵻�0
fixed IsInRect(half4 rect, half2 point2)
{
	half width = rect.z * 0.5;
	half height = rect.w * 0.5;
	fixed left = step(rect.x - width, point2.x);
	fixed right = step(point2.x, rect.x + width);
	fixed up = step(rect.y - height, point2.y);
	fixed down = step(point2.y, rect.y + height);
	return left * right * up * down;
}

//��һ�����Ƿ���ָ��Բ�������ڣ����򷵻�1�����򷵻�0
fixed IsInCircle(half2 center, half radius, half2 point2)
{
	half dis = distance(point2, center);
	return step(dis, radius);
}

//Ϊһ����ɫӦ������
half3 ApplyBrightness(half3 color, fixed brightness)
{
	return color * brightness;
}

//Ϊһ����ɫӦ�ñ��Ͷ�
half3 ApplySaturation(half3 color, fixed saturation)
{
	half gray = dot(half3(0.2154, 0.7154, 0.0721), color);
	half3 grayColor = half3(gray, gray, gray);
	return lerp(grayColor, color, saturation);
}

//Ϊһ����ɫӦ�öԱȶ�
half3 ApplyContrast(half3 color, fixed contrast)
{
	half3 contColor = half3(0.5, 0.5, 0.5);
	return lerp(contColor, color, contrast);
}

//Ϊһ��uvֵӦ�����ػ�����
float2 ApplyPixel(float2 uv, fixed pixelSize, float texelSize)
{
	//�˴�ȷ������ϵ��ʼ�մ��ڵ���5
	half factor = max(5, (1 - pixelSize) * texelSize);
	//��uvֵ��������ϵ����Ȼ��ȡ�����ٳ�������ϵ�����Դﵽ��������ϸ�������Ч��
	//���pixelSizeС�ڵ���0�����������ػ���ֱ�ӷ���ԭʼuv
	return If(step(pixelSize, 0), uv, round(uv * factor) / factor);
}

//��һ����ɫ����
half4 ApplyCoolColor(half4 color, fixed intensity)
{
	color.r *= (1 - intensity);
	color.b *= (1 + intensity);
	return color;
}

//��һ����ɫ��ů
half4 ApplyWarmColor(half4 color, fixed intensity)
{
	color.r *= (1 + intensity);
	color.b *= (1 - intensity);
	return color;
}

//Ϊһ����ɫӦ�÷���Ч��
half4 ApplyBloom(half4 color, half alpha, fixed threshold, fixed intensity, fixed3 bloomColor)
{
	color.rgb += bloomColor * saturate(1 - abs(threshold - alpha) * lerp(5, 1, intensity));
	return color;
}

//Ϊһ��uv����Ӧ��ģ��Ч��
half4 ApplyBlur(sampler2D mainTex, float2 pixelSize, float2 uv, int intensity)
{
	float4 color = float4(0.0, 0.0, 0.0, 0.0);
	int count = 0;
	for (int i = -intensity; i <= intensity; i++)
	{
		for (int j = -intensity; j <= intensity; j++)
		{
			color += tex2D(mainTex, float2(uv.x + i * pixelSize.x, uv.y + j * pixelSize.y));
			count += 1;
		}
	}
	return color / count;
}

//Ϊһ��uv����Ӧ��������Ч�����������������Խ������Խ��
half4 ApplyShiny(half4 color, float2 uv, fixed width, fixed softness, fixed brightness, fixed gloss)
{
	//�Ƚ������uv����[0,0.5,1]��ӳ�䵽����[1,0,1]������widthϵ����������
	//Ȼ��ͨ��1��ȥ���䣬��value����Ϊ����[0,1,0]
	half value = 1 - saturate(abs((uv.x * 2 - 1) / (width * 2)));
	//ͨ��smoothstep������[0,1,0]ƽ����������һ��ǿ�ȵõ�����ǿ��power
	half power = smoothstep(0, softness * 2, value) * 0.5;
	//ͨ������Ȳ�ֵ�õ�������ɫshinyColor
	half3 shinyColor = lerp(fixed3(1, 1, 1), color.rgb * 20, gloss);
	//��ԭ��ɫ�����ϵ���������ɫ
	color.rgb += color.a * power * brightness * shinyColor;
	return color;
}

//Ϊһ��uv����Ӧ��ɨ��Ч��
half4 ApplyScan(half4 color, float2 uv, fixed scanPos, fixed scanWidth, fixed4 scanColor, half scanIntensity, int scanDensity, sampler2D noiseTex)
{
	//�������������������ɨ��ǿ��
	float2 scanUV = round(uv * scanDensity) / scanDensity;
	scanIntensity = tex2D(noiseTex, scanUV + float2(_Time.y, _Time.y)).a * scanIntensity;

	//����ɨ������ƽ��ɨ����ɫ
	fixed left = scanPos - scanWidth;
	fixed right = scanPos;
	fixed factor = step(left, uv.x) * step(uv.x, right);
	scanColor = smoothstep(left, right, uv.x) * scanColor * scanIntensity * factor;

	color += scanColor;
	return color;
}

//Ϊһ����ɫӦ���ܽ�Ч��
half4 ApplyDissolve(half4 color, fixed3 dissolveColor, half alpha, fixed degree, fixed width, fixed softness)
{
	//���ſ��ϵ��
	width *= 0.1;
	//ֻҪ�ܽ�̶�degreeС��0.01���򽫿��width����Ͷ�softness��Ϊ0����ֹ�ܽ�̶�Ϊ0ʱ��Ȼ���ܽ�Ч��
	fixed value = step(0.01, degree);
	width *= value;
	softness *= value;
	
	//colorFactor �ܽ���ɫ���ӣ���colorFactor����0ʱ�������ڡ��ܽ��С������򣬷�֮���ڡ����ܽ⡿��δ�ܽ⡿����
	float colorFactor = width - abs(degree - alpha);
	colorFactor = saturate(colorFactor * 20 / softness);
	//alphaFactor �ܽ�͸�������ӣ���alphaFactor����0ʱ�������ڡ��ܽ��С���δ�ܽ⡿�����򣬷�֮���ڡ����ܽ⡿������
	float alphaFactor = width - (degree - alpha);
	alphaFactor = saturate(alphaFactor * 20 / softness);

#if _MODE_BLEND
	//�������ܽ��е�����ʱ������ܽ�ɫ�����򲻻����ɫ
	color.rgb += dissolveColor * colorFactor;
#endif

#if _MODE_OVERLAY
	//�������ܽ��е�����ʱ�������ܽ�ɫ�����򲻸�����ɫ
	color.rgb = lerp(color.rgb, dissolveColor, colorFactor);
#endif

	//�������ܽ��С���δ�ܽ�ʱ��͸���ȵ��ӣ�����͸����Ϊ0
	color.a *= alphaFactor;
	//���ܽ�̶�Ϊ1ʱ��͸��������Ϊ0
	color.a *= (1 - step(1, degree));

	return color;
}

//Ϊһ��uv����Ӧ�ñ߿�����
half4 ApplyBorderFlow(half4 color, float2 uv, half flowPos, half flowWidth, half flowThickness, half flowBrightness, fixed3 flowColor, float2 texelSize)
{
	//�������±߿�Ŀ���
	half width = flowWidth * 0.5;
	half height = flowThickness * 0.5;

	//�����ϱ߿�
	//���㵱ǰ����λ��
	half ratio = smoothstep(-width, 0.5, If(step(flowPos, 0.5), flowPos, flowPos - 1));
	//������ӳ�䵽ͼ���ϵ���ʵλ��
	half realPos = lerp(width * -1, 1 + width, ratio);
	//���㵱ǰ����ǿ��
	half brightness = IsInRect(half4(realPos, 1 - height, width * 2, height * 2), uv) * flowBrightness;
	//����������ƽ����ʹ��Խ���������Ҳ࣬����ǿ��Խ�ӽ�1��Խ����������࣬����ǿ��Խ�ӽ�0��
	brightness *= smoothstep(0, width * 2, uv.x - realPos + width);
	//��������ɫ���ӵ�����ɫ
	color.rgb += color.a * brightness * flowColor;

	//�����±߿�ԭ��ͬ�ϱ߿�
	realPos = lerp(width * -1, 1 + width, 1 - ratio);
	brightness = IsInRect(half4(realPos, height, width * 2, height * 2), uv) * flowBrightness;
	brightness *= smoothstep(0, width * 2, realPos - uv.x + width);
	color.rgb += color.a * brightness * flowColor;

	//�������ұ߿�Ŀ��ߣ���֤��ͼ��Ŀ��߲���ʱ������Ŀ���ֵ����һ�£�
	width = width * texelSize.x / texelSize.y;
	height = height * texelSize.y / texelSize.x;

	//������߿�ԭ��ͬ�ϱ߿�
	ratio = smoothstep(0.5 - width, 1, flowPos);
	realPos = lerp(width * -1, 1 + width, ratio);
	brightness = IsInRect(half4(height, realPos, height * 2, width * 2), uv) * flowBrightness;
	brightness *= smoothstep(0, width * 2, uv.y - realPos + width);
	color.rgb += color.a * brightness * flowColor;

	//�����ұ߿�ԭ��ͬ�ϱ߿�
	realPos = lerp(width * -1, 1 + width, 1 - ratio);
	brightness = IsInRect(half4(1 - height, realPos, height * 2, width * 2), uv) * flowBrightness;
	brightness *= smoothstep(0, width * 2, realPos - uv.y + width);
	color.rgb += color.a * brightness * flowColor;

	return color;
}

//Ϊһ��uv����Ӧ�÷����ο�
half4 ApplyCubePierced(half4 color, float2 uv, half4 piercedRect, fixed alpha)
{
	fixed value = IsInRect(piercedRect, uv);
	color.a = alpha * value + color.a * (1 - value);
	return color;
}

//Ϊһ��uv����Ӧ��Բ���ο�
half4 ApplyCirclePierced(half4 color, float2 uv, half2 center, half radius, fixed alpha)
{
	fixed value = IsInCircle(center, radius, uv);
	color.a = alpha * value + color.a * (1 - value);
	return color;
}

//Ϊһ��uv����Ӧ�ò���Ч��
half4 ApplyWave(sampler2D mainTex, sampler2D noiseTex, float2 uv, float2 wave, fixed intensity)
{
	half4 noise = tex2D(noiseTex, uv + wave);
	half4 color = tex2D(mainTex, uv + noise.a * intensity);
	return color;
}

//Ϊһ����ɫӦ������Ч��
half3 ApplyCorrect(half3 color, float targetHue, float correctHue, float differenceHue)
{
	float3 hsv = RGBToHSV(color);
	//����ɫ��
	float difference = hsv.x - targetHue;
	//ɫ��ֵС�����ɫ��
	fixed isCorrect = step(abs(difference), differenceHue);
	//������ɫ
	hsv.x = If(isCorrect, correctHue + difference, hsv.x);
	color = HSVToRGB(hsv);
	return color;
}

//���㴦���������ݣ���׼��
struct VertData
{
	float4 vertex   : POSITION;
	fixed4 color    : COLOR;
	float2 texcoord : TEXCOORD0;
	UNITY_VERTEX_INPUT_INSTANCE_ID
};

//ƬԪ�����������ݣ���׼��
struct FragData
{
	float4 vertex   : SV_POSITION;
	fixed4 color : COLOR;
	float2 texcoord  : TEXCOORD0;
	float4 worldPosition : TEXCOORD1;
	UNITY_VERTEX_OUTPUT_STEREO
};

//���㴦��������׼��
FragData vert(VertData IN)
{
	FragData OUT;
	UNITY_SETUP_INSTANCE_ID(IN);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
	OUT.worldPosition = IN.vertex;
	OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
	OUT.texcoord = IN.texcoord;
	OUT.color = IN.color;
	return OUT;
}

#endif