# FoodLens

## What This Is

FoodLens is an adaptive mobile AR application built in Unity 2022 LTS that enables users to view photorealistic, 1:1 scale 3D food models (such as burgers, pizzas, and biryani bowls) placed on horizontal surfaces. The app employs a dynamic 3-tier capability engine supporting High-End devices (Full AR via ARCore), Mid-Range devices (Lite AR with fixed ground placement and optimized visuals), and Low-End devices (Fake AR utilizing a webcam camera feed and fixed 3D screen overlay with drop shadows).

## Core Value

Enable users on *any* Android device to experience a realistic 1:1 preview of restaurant dishes before ordering, driving engagement and conversion through accessible spatial visualization.

## Requirements

### Validated

(None yet — ship to validate)

### Active

- [ ] **AR-01**: Dynamic device capability check and 3-tier mode routing (Full AR, Lite AR, Fake AR).
- [ ] **AR-02**: Horizontal plane detection with grid overlay and ±5 cm accuracy (Full AR Mode).
- [ ] **AR-03**: Tap-to-place food placement with short vibration haptic feedback and world anchoring (Full AR Mode).
- [ ] **AR-04**: Real-time light estimation adjusting virtual directional light intensity and color temperature (Full AR Mode).
- [ ] **AR-05**: Dynamic real-time shadows via shadow catcher and ambient occlusion (Full AR Mode).
- [ ] **AR-06**: Fixed ground placement and basic light source visualization (Lite AR Mode).
- [ ] **AR-07**: Webcam background feed overlay with fake depth/blur and blob shadows (Fake AR Mode).
- [ ] **FOOD-01**: True 1:1 scale models of Burger (12cm), Pizza (30cm), and Biryani Bowl (20cm) with poly budget <= 15k triangles.
- [ ] **FOOD-02**: PBR rendering with neutral Albedo, Normal maps, Roughness, and Metallic maps.
- [ ] **GEST-01**: One-finger drag for 360-degree Y-axis rotation.
- [ ] **GEST-02**: Pinch gesture for scale clamping (0.8x to 1.2x of 1:1 real-world size).
- [ ] **GEST-03**: Drag gesture to reposition placed food along the detected surface.
- [ ] **GEST-04**: Double-tap to open removal confirmation dialog and clear objects.
- [ ] **UI-01**: Setup guide flow instructing user to move phone slowly to scan surfaces.
- [ ] **UI-02**: swipeable bottom food carousel with thumbnails and dish details (price, name, calories).
- [ ] **UI-03**: Interactive panel with Add to Cart and Urdu/English Family Mode language toggle.

### Out of Scope

- [ ] Cloud-based assets streaming (all models pre-loaded in app package for MVP).
- [ ] Direct payment processing (out of scope for visualization MVP; add-to-cart acts as mock integration).

## Context

- Target Platform: Android (Unity 2022.3 LTS, ARCore/AR Foundation).
- Optimizations: Target stable 60 FPS on mid-to-high end devices and 30+ FPS on low-end devices. Max RAM usage <= 150 MB.
- Dual language support (Urdu/English) in UI for Family Mode usability.

## Constraints

- **Tech Stack**: Unity 2022 LTS, AR Foundation 5.x, Universal Render Pipeline (URP).
- **Android Support**: Min SDK API Level 24 (Android 7.0) for ARCore.
- **Hardware Profile**: Low-end support via Fake AR fallback to ensure no crash or device exclusion.

## Key Decisions

| Decision | Rationale | Outcome |
|----------|-----------|---------|
| 3-Tier Adaptive Engine | ARCore devices are limited; fallback modes prevent user exclusion | — Pending |
| URP Shadow Catcher | Enables high fidelity shadows on real surfaces in Full AR | — Pending |
| Dual Urdu/English Toggle | Enhances accessibility for the "Family Mode" emotional feature | — Pending |

## Evolution

This document evolves at phase transitions and milestone boundaries.

**After each phase transition** (via `/gsd-transition`):
1. Requirements invalidated? → Move to Out of Scope with reason
2. Requirements validated? → Move to Validated with phase reference
3. New requirements emerged? → Add to Active
4. Decisions to log? → Add to Key Decisions
5. "What This Is" still accurate? → Update if drifted

**After each milestone** (via `/gsd-complete-milestone`):
1. Full review of all sections
2. Core Value check — still the right priority?
3. Audit Out of Scope — reasons still valid?
4. Update Context with current state
