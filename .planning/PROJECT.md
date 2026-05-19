# FoodLens Lite

## What This Is

FoodLens Lite is an adaptive mobile AR application built in Unity that lets users place 1:1 scale, photorealistic 3D food models onto real-world surfaces. It features a smart 3-tier system (Full AR, Lite AR, Fake AR) that detects hardware capabilities at startup, enabling photorealistic AR on high-end devices while falling back to a camera-overlay 3D renderer on low-end devices.

## Core Value

Provide a 1:1 photorealistic food visualization experience to all users, regardless of their device's hardware capabilities, through an adaptive rendering engine that ensures massive adoption and high performance.

## Requirements

### Validated

(None yet — ship to validate)

### Active

- [ ] **REQ-SYS-01**: Device capability detection (ARCore, RAM, GPU, CPU cores) to assign rendering tier at startup.
- [ ] **REQ-AR-01**: Full AR Mode (ARCore) with horizontal surface detection, transparent grid overlay, and tap-to-place world anchoring.
- [ ] **REQ-AR-02**: Real-time light estimation and ambient probe matching for Full AR.
- [ ] **REQ-AR-03**: Dynamic shadow catcher and ambient occlusion for realistic ground contact.
- [ ] **REQ-LITE-01**: Lite AR Mode (ARCore) with simplified tracking, simple lighting, and disabled real-time shadows.
- [ ] **REQ-FAKE-01**: Fake AR Mode for low-end devices utilizing camera feed background overlay and screen-fixed 3D rendering.
- [ ] **REQ-GEST-01**: Touch gesture control system (1-finger drag rotate, pinch scale locked 0.8x-1.2x, drag reposition, double-tap remove).
- [ ] **REQ-UI-01**: Restaurant-ready menu UI with bottom swipeable carousel, ghost preview mode, and detail info panel (Price, Calories, Add to Cart).
- [ ] **REQ-UI-02**: Family Mode with big buttons, simplified navigation, and Urdu/English language toggle.
- [ ] **REQ-OPT-01**: Dynamic performance fallback (thermal/FPS monitoring dropping Full AR to Lite AR if FPS drops below 30).
- [ ] **REQ-ASSET-01**: 3D food model assets (Burger, Pizza slice, Biryani bowl) with multi-LOD meshes (15k, 7k, 3k polys) and custom PBR materials.

### Out of Scope

- **Multi-user AR Sharing**: Excluded because it requires server-side state synchronization, which increases network complexity beyond the MVP.
- **Dynamic Cloud Photogrammetry Generation**: Excluded because processing photogrammetry meshes at runtime requires cloud computing pipelines. All meshes are pre-processed and embedded.
- **In-App Payment Gateway Integration**: Excluded for MVP; order placement is limited to "Add to Cart" and sending a localized order message/payload.

## Context

- Target OS: Android (API level 24 / Nougat or higher).
- Engine: Unity 2022 LTS (using Universal Render Pipeline / URP).
- SDKs: AR Foundation 5.x, ARCore XR Plugin.
- High-quality asset rendering relies heavily on PBR materials (Albedo, Normal, Roughness, and Metallic maps) and runtime lighting estimation.

## Constraints

- **Performance**: High-end Full AR mode must maintain a stable 60 FPS on Snapdragon 700 / 6GB RAM devices.
- **Scale**: Strict 1:1 physical scaling (e.g., Burger = 0.12m, Pizza = 0.3m) with ±5 cm placement accuracy.
- **Memory**: The app must stay under a 150 MB RAM budget for all active assets to prevent crashes on low-end hardware.

## Key Decisions

| Decision | Rationale | Outcome |
|----------|-----------|---------|
| 3-Tier Adaptive AR | Low-end phones fail to support ARCore; Fake AR camera overlay ensures 100% device compatibility. | — Pending |
| Unity Universal Render Pipeline (URP) | URP is optimized for mobile performance and provides clean light estimation & shadow catcher support. | — Pending |
| PBR Materials with LOD versions | Balances visual quality with vertex budget constraints across different device tiers. | — Pending |

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

---
*Last updated: 2026-05-20 after initialization*
