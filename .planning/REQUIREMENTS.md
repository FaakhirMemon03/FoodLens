# Requirements: FoodLens

**Defined:** 2026-05-20
**Core Value:** Enable users on *any* Android device to experience a realistic 1:1 preview of restaurant dishes before ordering, driving engagement and conversion through accessible spatial visualization.

## v1 Requirements

Requirements for initial release. Each maps to roadmap phases.

### AR Engine & Surface Detection

- [ ] **AR-01**: Dynamic device capability check and 3-tier mode routing (Full AR, Lite AR, Fake AR).
- [ ] **AR-02**: Horizontal plane detection with transparent grid overlay and ±5 cm accuracy (Full AR Mode).
- [ ] **AR-03**: Tap-to-place food placement with short vibration haptic feedback and world anchoring (Full AR Mode).
- [ ] **AR-04**: Real-time light estimation adjusting virtual directional light intensity and color temperature (Full AR Mode).
- [ ] **AR-05**: Dynamic real-time shadows via shadow catcher plane and ambient occlusion (Full AR Mode).
- [ ] **AR-06**: Fixed ground plane placement and basic light source visualization (Lite AR Mode).
- [ ] **AR-07**: Webcam background feed overlay with fake depth/blur and blob shadows (Fake AR Mode).

### 3D Food Visualization Standards

- [ ] **FOOD-01**: True 1:1 scale models of Burger (12cm), Pizza (30cm), and Biryani Bowl (20cm) with poly budget <= 15k triangles.
- [ ] **FOOD-02**: PBR rendering with neutral Albedo, Normal maps, Roughness, and Metallic maps.

### Interaction System (Gestures)

- [ ] **GEST-01**: One-finger drag for 360-degree Y-axis rotation.
- [ ] **GEST-02**: Pinch gesture for scale clamping (0.8x to 1.2x of 1:1 real-world size).
- [ ] **GEST-03**: Drag gesture to reposition placed food along the detected surface.
- [ ] **GEST-04**: Double-tap to open removal confirmation dialog and clear objects.

### UI/UX Flow & Accessibility

- [ ] **UI-01**: Setup guide flow instructing user to move phone slowly to scan surfaces.
- [ ] **UI-02**: Swipeable bottom food carousel with thumbnails and dish details (price, name, calories).
- [ ] **UI-03**: Interactive panel with Add to Cart and Urdu/English Family Mode language toggle.

## v2 Requirements

Deferred to future release. Tracked but not in current roadmap.

### Customization & Social

- **CUST-01**: Ingredient customization (add/remove cheese, etc.) updating the visual model.
- **SOCL-01**: Screenshot capture with custom restaurant watermark and native share dialog.
- **PART-01**: Particle system for food steam / sizzle effects.

## Out of Scope

Explicitly excluded. Documented to prevent scope creep.

| Feature | Reason |
|---------|--------|
| Cloud Asset Streaming | All assets pre-loaded in the build to avoid network latency and simplify MVP |
| Direct Payment Processing | High compliance/integration overhead; Add-to-cart acts as mock integration |

## Traceability

Which phases cover which requirements. Updated during roadmap creation.

| Requirement | Phase | Status |
|-------------|-------|--------|
| AR-01 | Phase 1 | Pending |
| AR-02 | Phase 2 | Pending |
| AR-03 | Phase 2 | Pending |
| AR-04 | Phase 2 | Pending |
| AR-05 | Phase 2 | Pending |
| AR-06 | Phase 3 | Pending |
| AR-07 | Phase 4 | Pending |
| FOOD-01 | Phase 5 | Pending |
| FOOD-02 | Phase 5 | Pending |
| GEST-01 | Phase 6 | Pending |
| GEST-02 | Phase 6 | Pending |
| GEST-03 | Phase 6 | Pending |
| GEST-04 | Phase 6 | Pending |
| UI-01 | Phase 7 | Pending |
| UI-02 | Phase 7 | Pending |
| UI-03 | Phase 7 | Pending |

**Coverage:**
- v1 requirements: 16 total
- Mapped to phases: 16
- Unmapped: 0 ✓

---
*Requirements defined: 2026-05-20*
*Last updated: 2026-05-20 after initial definition*
