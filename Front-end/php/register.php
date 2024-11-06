<?php
// register.php

// Configuración de la base de datos
$host = 'localhost';
$dbname = 'cartsking';
$username = 'your_username';
$password = 'your_password';

try {
    // Conectar a la base de datos
    $pdo = new PDO("mysql:host=$host;dbname=$dbname", $username, $password);
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    
    // Obtener datos del POST request
    $data = json_decode(file_get_contents('php://input'), true);
    
    // Validar datos
    if (!isset($data['fullname']) || !isset($data['email']) || !isset($data['phone']) || !isset($data['password'])) {
        throw new Exception('Missing required fields');
    }
    
    // Verificar si el email ya existe
    $stmt = $pdo->prepare("SELECT id FROM users WHERE email = ?");
    $stmt->execute([$data['email']]);
    if ($stmt->fetch()) {
        echo json_encode(['success' => false, 'message' => 'Email already exists']);
        exit;
    }
    
    // Hash de la contraseña
    $hashedPassword = password_hash($data['password'], PASSWORD_DEFAULT);
    
    // Insertar nuevo usuario
    $stmt = $pdo->prepare("INSERT INTO users (fullname, email, phone, password) VALUES (?, ?, ?, ?)");
    $stmt->execute([
        $data['fullname'],
        $data['email'],
        $data['phone'],
        $hashedPassword
    ]);
    
    echo json_encode(['success' => true, 'message' => 'Registration successful']);
    
} catch (Exception $e) {
    echo json_encode(['success' => false, 'message' => $e->getMessage()]);
}
?>