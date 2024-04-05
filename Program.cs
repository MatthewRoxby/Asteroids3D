using engine;
using scenes;

GameManager.EDIT_MODE = args.Contains("EDIT_MODE");

Window window = new Window(800,800, "Asteroids", new TestScene());
