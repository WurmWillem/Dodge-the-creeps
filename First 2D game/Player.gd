extends Area2D

signal hit

export var speed = 400
var screenSize = Vector2.ZERO

func _ready():
	screenSize = get_viewport_rect().size

func _process(delta):
	var direction =  Vector2.ZERO
	if Input.is_action_pressed("move_right"):
		direction.x += 1
	if Input.is_action_pressed("move_left"):
		direction.x -= 1	
	if Input.is_action_pressed("move_up"):
		direction.y -= 1	
	if Input.is_action_pressed("move_down"):
		direction.y += 1	
		
	if direction.length() > 1:
		direction = direction.normalized();	
		
	if direction.length() > 0:
		direction = direction.normalized();	
		$AnimatedSprite.play()
	else:
		$AnimatedSprite.stop()	
		
	if direction.x != 0:
		$AnimatedSprite.flip_v = false
		$AnimatedSprite.animation = "right"
		$AnimatedSprite.flip_h = direction.x < 0
	if direction.y != 0:
		$AnimatedSprite.animation = "up"
		#$AnimatedSprite.flip_v = false
		$AnimatedSprite.flip_v = direction.y > 0		
		
	position += direction * speed * delta	
	position.x = clamp(position.x,0,screenSize.x)
	position.y = clamp(position.y,0,screenSize.y)
	
#func start(new_position):
#	position = new_position
#	show()	
#	$CollisionShape2D.disabled = false
	
# warning-ignore:unused_argument
func _on_Player_body_entered(body):
	hide()
	$CollisionShape2D.set_deferred("disabled", true)
	emit_signal("hit")
	
	
	
	
	
