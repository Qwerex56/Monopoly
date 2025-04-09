shader_type canvas_item;

uniform float border_size = 2.0;

bool is_outside_range(float value, float a, float b) {
	return value < a || value > b;
}

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	ivec2 tex_size = textureSize(TEXTURE, 0);

	float uv_x_position = UV.x * float(tex_size.x);
	float uv_y_position = UV.y * float(tex_size.y);

	// Called for every pixel the material is visible on.
	if (is_outside_range(uv_x_position, border_size, float(tex_size.x) - border_size) ||
		is_outside_range(uv_y_position, border_size, float(tex_size.y) - border_size)) {
		COLOR = vec4(vec3(0.0), 1.0);
	} else {
		COLOR = COLOR;
	}
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
