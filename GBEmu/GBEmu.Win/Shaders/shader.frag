#version 330

out vec4 outputColor;
in vec3 vertexColor;

void main()
{
    outputColor = vec4(vertexColor, 1.0);
}