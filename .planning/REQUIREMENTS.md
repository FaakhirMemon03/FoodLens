# Requirements: FoodLens Lite

**Defined:** 2026-05-20
**Core Value:** Provide a 1:1 photorealistic food visualization experience to all users, regardless of their device's hardware capabilities, through an adaptive rendering engine that ensures massive adoption and high performance.

## v1 Requirements

### Device Detection & System (SYS)

- [ ] **SYS-01**: The app must check for ARCore device compatibility on startup.
- [ ] **SYS-02**: The app must query RAM, GPU memory, and CPU core count to calculate a device performance score.
- [ ] **SYS-03**: The app must route the user to either Full AR, Lite AR, or Fake AR mode based on ARCore support and capability scoring.
- [ ] **SYS-04**: The app must monitor runtime FPS and perform a thermal/performance fallback from Full AR to Lite AR if FPS drops below 30.
- [ ] **SYS-05**: The app must allow manual user overrides to toggle between performance mode (Lite) and quality mode (Full).

### Full AR Engine (AR)

- [ ] **AR-01**: AR Plane Manager must detect and visual-grid horizontal planes (tables, counters) with ±5 cm accuracy.
- [ ] **AR-02**: Tap-to-place with haptic feedback must spawn the selected food model anchored on the detected plane.
- [ ] **AR-03**: Real-time lighting estimation must update ambient lighting probe and directional light intensity/color from camera frame feed.
- [ ] **AR-04**: Food models must cast shadows on a transparent shadow catcher plane aligned with the detected surface.

### Lite AR Engine (LITE)

- [ ] **LITE-01**: Basic plane detection must allow tap-to-place with reduced tracking computations.
- [ ] **LITE-02**: Real-time shadows must be disabled in Lite AR mode to conserve GPU bandwidth.
- [ ] **LITE-03**: Lighting must be driven by standard, simplified directional setups.

### Fake AR Engine (FAKE)

- [ ] **FAKE-01**: The app must project a live camera background feed using `WebCamTexture` on a background plane.
- [ ] **FAKE-02**: The app must render the selected 3D food item anchored at a fixed distance in front of the main camera.
- [ ] **FAKE-03**: The app must render a blob shadow texture directly beneath the food model to simulate ground contact.

### Gesture Controls (GEST)

- [ ] **GEST-01**: Single-finger drag must rotate the placed food model 360 degrees around the Y-axis.
- [ ] **GEST-02**: Two-finger pinch must scale the food model strictly between 0.8x and 1.2x.
- [ ] **GEST-03**: Repositioning must be triggered by long-press followed by drag, constraining movement to the surface plane.
- [ ] **GEST-04**: Double-tap must trigger a confirmation overlay to remove the model.

### User Interface & Family Mode (UI)

- [ ] **UI-01**: The app must display a splash screen requesting camera permissions and instructions for surface scanning.
- [ ] **UI-02**: Bottom carousel must allow swipe-selection of menu items (Burger, Pizza, Biryani) with thumb-previews.
- [ ] **UI-03**: Placed food items must trigger a slide-up info panel displaying Name, Price, Calories, and an "Add to Cart" button.
- [ ] **UI-04**: Family Mode toggle must switch UI buttons to extra-large sizes and toggle text between English and Urdu.

### 3D Assets & Materials (ASSET)

- [ ] **ASSET-01**: 3D food models for Burger, Pizza slice, and Biryani bowl must have three LOD levels (LOD0: 15k, LOD1: 7k, LOD2: 3k polygons).
- [ ] **ASSET-02**: Assets must be scaled to 1:1 real-world size (1 unit = 1 meter).
- [ ] **ASSET-03**: Food materials must be standard PBR Lit with 2K textures for Full AR, 1K for Lite, and 512px for Fake AR.

## Out of Scope

| Feature | Reason |
|---------|--------|
| Multi-user Shared AR | High networking complexity. Out of scope for MVP. |
| Runtime Photogrammetry | Generating 3D meshes from photos at runtime is computationally too heavy for mobile devices. |
| In-App Payments | Payment processing is deferred to future integrations. |

## Traceability

| Requirement | Phase | Status |
|-------------|-------|--------|
| SYS-01 | Phase 1: Engine Foundation & Core Detection | Pending |
| SYS-02 | Phase 1: Engine Foundation & Core Detection | Pending |
| SYS-03 | Phase 1: Engine Foundation & Core Detection | Pending |
| SYS-04 | Phase 4: Optimizations & Polishing | Pending |
| SYS-05 | Phase 3: Menu UI & Family Mode | Pending |
| AR-01 | Phase 2: Full AR & Lite AR Development | Pending |
| AR-02 | Phase 2: Full AR & Lite AR Development | Pending |
| AR-03 | Phase 2: Full AR & Lite AR Development | Pending |
| AR-04 | Phase 2: Full AR & Lite AR Development | Pending |
| LITE-01 | Phase 2: Full AR & Lite AR Development | Pending |
| LITE-02 | Phase 2: Full AR & Lite AR Development | Pending |
| LITE-03 | Phase 2: Full AR & Lite AR Development | Pending |
| FAKE-01 | Phase 1: Engine Foundation & Core Detection | Pending |
| FAKE-02 | Phase 1: Engine Foundation & Core Detection | Pending |
| FAKE-03 | Phase 1: Engine Foundation & Core Detection | Pending |
| GEST-01 | Phase 2: Full AR & Lite AR Development | Pending |
| GEST-02 | Phase 2: Full AR & Lite AR Development | Pending |
| GEST-03 | Phase 2: Full AR & Lite AR Development | Pending |
| GEST-04 | Phase 2: Full AR & Lite AR Development | Pending |
| UI-01 | Phase 3: Menu UI & Family Mode | Pending |
| UI-02 | Phase 3: Menu UI & Family Mode | Pending |
| UI-03 | Phase 3: Menu UI & Family Mode | Pending |
| UI-04 | Phase 3: Menu UI & Family Mode | Pending |
| ASSET-01 | Phase 4: Optimizations & Polishing | Pending |
| ASSET-02 | Phase 4: Optimizations & Polishing | Pending |
| ASSET-03 | Phase 4: Optimizations & Polishing | Pending |

**Coverage:**
- v1 requirements: 26 total
- Mapped to phases: 26
- Unmapped: 0 ✓

---
*Requirements defined: 2026-05-20*
*Last updated: 2026-05-20 after initial definition*
