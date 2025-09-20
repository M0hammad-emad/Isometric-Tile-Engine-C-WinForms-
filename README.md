# Isometric Tile Engine (C# WinForms)

This project is a small **isometric tile-based world editor** built with **C# WinForms**.  
It demonstrates how to render tiles in an isometric perspective, detect the tile under the mouse, and update the map interactively.

## 🧩 What is Isometric?
In computer graphics, **isometric projection** is a way of drawing 2D tiles so they appear **3D-like**.  
Instead of square tiles, the map is made of **diamond-shaped tiles** (e.g., 80x40).  
This gives the illusion of depth, making it look like you are looking at the world from an angle.  
Many classic games (like SimCity, Age of Empires, and Diablo) use isometric graphics.

## 🚀 Project Description
- The world is made of a **grid of isometric tiles**.  
- Each tile is drawn using a sprite (`isometric_1.png` to `isometric_6.png`).  
- A **mask image** (`isometric_musk.png`) is used to detect which part of the diamond shape the mouse is pointing at.  
- When you **move the mouse**, the tile under the cursor is highlighted.  
- When you **click a tile**, it cycles through different tile types (e.g., ground variations).  
- Rendering uses **double buffering** to keep the animation smooth.

## 🎮 Controls
- **Mouse Move** → Highlight a tile  
- **Left Click** → Change the tile type 
