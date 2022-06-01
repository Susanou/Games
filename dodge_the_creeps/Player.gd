extends Area2D

export var speed = 400 # speed of player in px.s^-1
var screen_size # size of the game window

func _ready():
	screen_size = get_viewport_rect().size
