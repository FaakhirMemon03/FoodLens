# Roadmap: FoodLens Lite

## Overview

This roadmap defines the transition of FoodLens Lite from an idea to a fully functional adaptive mobile AR application. The build sequence is structured to establish compatibility first (Fake AR & Device Detection), follow up with standard AR tracking and input gestures (Full & Lite AR), layer the localized UI (Urdu/English menu & Family Mode), and conclude with asset-level LOD optimizations and runtime FPS drop fallback controls.

## Phases

- [ ] **Phase 1: Engine Foundation & Core Detection** - Set up device capability checking, scene routing, and Fake AR camera-overlay rendering.
- [ ] **Phase 2: AR Engines & Interaction Systems** - Set up AR Foundation scenes, placement systems, lighting estimation, and touch gesture controls.
- [ ] **Phase 3: Menu UI & Family Mode** - Implement the swipeable bottom menu carousel, slides, detail overlays, and English/Urdu Family Mode layouts.
- [ ] **Phase 4: Assets, Fallback & Optimizations** - Import multi-LOD food prefabs, build runtime FPS tracking, and implement automatic rendering downgrade logic.

## Phase Details

### Phase 1: Engine Foundation & Core Detection
**Goal**: Establish the startup device checking logic and a robust fallback Fake AR camera-overlay renderer to ensure 100% device compatibility.
**Depends on**: Nothing
**Requirements**: SYS-01, SYS-02, SYS-03, FAKE-01, FAKE-02, FAKE-03
**Success Criteria**:
  1. The app starts and successfully queries system RAM, CPU cores, and GPU capacity.
  2. The app detects ARCore support and routes to the correct mode scene.
  3. On low-end systems, Fake AR mode launches, activating the device camera as a background texture with a 3D food item placed relative to the camera viewport.
  4. User can drag and scale the food item in Fake AR.
**Plans**: 3 plans

Plans:
- [x] 01-01: Setup folder structure and Mode Detection System (C# scripts).
- [x] 01-02: Implement Boot Loader scene and routing to mode scenes.
- [x] 01-03: Implement Fake AR controller with camera background rendering and screen-anchored object controls.

### Phase 2: AR Engines & Interaction Systems
**Goal**: Build the AR Core and Lite AR placement engines using AR Foundation, and implement gesture rotation/scaling controls.
**Depends on**: Phase 1
**Requirements**: AR-01, AR-02, AR-03, AR-04, LITE-01, LITE-02, LITE-03, GEST-01, GEST-02, GEST-03, GEST-04
**Success Criteria**:
  1. Full AR mode detects horizontal planes and renders a transparent tracking grid.
  2. Tapping a grid plane anchors the selected food object at a 1:1 physical scale.
  3. Real-time light estimation alters directional light intensity to match local physical environments.
  4. Placed food items cast realistic shadows on a shadow catcher plane.
  5. Touch gestures smoothly rotate (1-finger), scale (pinch, restricted to 0.8x-1.2x), and reposition (long-press drag) objects.
**Plans**: 3 plans

Plans:
- [x] 02-01: Set up Full AR scene with AR Foundation and implement Placement Manager.
- [x] 02-02: Integrate Lighting Estimation and Shadow Catcher systems.
- [x] 02-03: Implement Interaction Handler script for rotation, scaling (clamped), repositioning, and deletion.

### Phase 3: Menu UI & Family Mode
**Goal**: Design and script the restaurant-themed user interface, incorporating Urdu/English toggles and Family Mode scaling overrides.
**Depends on**: Phase 2
**Requirements**: SYS-05, UI-01, UI-02, UI-03, UI-04
**Success Criteria**:
  1. Application displays scanning UI overlay during environment scanning.
  2. Bottom carousel allows swipe selection of menu items (Burger, Pizza, Biryani).
  3. Selecting an item spawns its AR representation, and slides up a details card with price, name, and "Add to Cart" button.
  4. Toggling Family Mode enlarges interactive buttons and switches text from English to Urdu.
**Plans**: 2 plans

Plans:
- [x] 03-01: Build the UI canvases (Splash, Scanning, Menu Carousel, Info Panel).
- [x] 03-02: Implement Localizer and UI Manager for language toggle and Family Mode sizing.

### Phase 4: Assets, Fallback & Optimizations
**Goal**: Integrate final 3D models with LOD meshes, compress textures, and program the FPS-based runtime fallback safety system.
**Depends on**: Phase 3
**Requirements**: SYS-04, ASSET-01, ASSET-02, ASSET-03
**Success Criteria**:
  1. Food items load with LOD setup (LOD0: 15k, LOD1: 7k, LOD2: 3k).
  2. RAM usage remains below 150 MB with active food items.
  3. When running in Full AR mode, if FPS drops below 30 for 3 consecutive seconds, the engine dynamically downgrades the visual settings to Lite AR mode without losing object placement.
**Plans**: 2 plans

Plans:
- [x] 04-01: Setup food prefabs with LOD Group and PBR textures.
- [x] 04-02: Implement FPS Monitoring and Auto-Downgrade Controller.

## Progress

**Execution Order:**
Phases execute in numeric order: 1 → 2 → 3 → 4

| Phase | Plans Complete | Status | Completed |
|-------|----------------|--------|-----------|
| 1. Engine Foundation & Core Detection | 3/3 | Complete | 2026-05-20 |
| 2. AR Engines & Interaction Systems | 3/3 | Complete | 2026-05-20 |
| 3. Menu UI & Family Mode | 2/2 | Complete | 2026-05-20 |
| 4. Assets, Fallback & Optimizations | 2/2 | Complete | 2026-05-20 |
