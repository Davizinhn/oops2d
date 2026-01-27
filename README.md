Oops2D is a silly little 2d game engine written in C# built on Raylib :3

## 🚧 Roadmap
Oops2D is under active development.  
Below is the current roadmap and progress tracker.

---

## Core Architecture
| Status | Item |
|------|------|
| ✅ | Game Singleton (only one game instance allowed) |
| ✅ | Scene2D |
| ✅ | Object2D |
| 🔄 | Transform2D |
| ⬜ | Component System (allow object2d extensions via components) |

---

## Scene / Object Management
| Status | Item |
|------|------|
| ✅ | Scene Object Lifecycle |
| 🔄 | Scene Switching and Loading |
| 🔄 | Object Hierarchy (parent-children interactions) |
| ⬜ | Object Tags (for filtering in big scenes) |

---

## Rendering
| Status | Item |
|------|------|
| ✅ | Sprite2D |
| ✅ | Rectangle2D |
| 🔄 | oops2d.rendering Package |
| ⬜ | Custom Camera2D Implementation |
| ⬜ | Render Order (layers) |
| ⬜ | Visibility Culling (for better optimization) |

---

## Text Rendering
| Status | Item |
|------|------|
| ✅ | Text2D |
| 🔄 | Font Loading |
| 🔄 | Bitmap Font Support |
| ⬜ | Text Alignment & Wrapping |

---

## UI Rendering
| Status | Item |
|------|------|
| 🔄 | UI Render Pass (camera-independent) |
| ⬜ | UIElement Base Class |
| ⬜ | Buttons / Labels |
| ⬜ | Anchors & Scaling |
| ⬜ | Basic Layout System |

---

## Math Module
| Status | Item |
|------|------|
| 🔄 | oops2d.math Package |
| 🔄 | Math Utils |
| ⬜ | Vector2 Utils |
| ⬜ | Rectangle Utils |

---

## Input System
| Status | Item |
|------|------|
| 🔄 | oops2d.input Package |
| ⬜ | Keyboard Input Manager |
| ⬜ | Mouse Helper |
| ⬜ | Action Mapping (maybe?) |

---

## Audio
| Status | Item |
|------|------|
| 🔄 | oops2d.audio Package |
| ⬜ | Sound Effects Helpers |
| ⬜ | Music Streaming |

---

## Caching
| Status | Item |
|------|------|
| ✅ | Image Cache |
| ✅ | Texture Cache |
| ⬜ | Audio Cache |
| ⬜ | Reference-based Auto Unload (unload when disposable) |

---

## Tooling
| Status | Item |
|------|------|
| 🔄 | Error Handling |
| ⬜ | Debug Mode |
| ⬜ | Documentation |
| ⬜ | Example Projects |

---

## Physics (Future)
| Status | Item |
|------|------|
| ⬜ | Physics Module |
| ⬜ | Box2D Integration |
| ⬜ | PhysicsBody2D |
| ⬜ | Collision Callbacks |

---

### Legend
- ✅ completed  
- 🔄 in progress  
- ⬜ planned  
