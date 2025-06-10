# Flight Sales System – Dokumentacja Domeny

## Opis poszczególnych USE CASES w projekcie

### 1. **Ręczne dodanie lotu**

- **Opis:** Możliwość ręcznego dodania nowego lotu.
- **Realizacja:**  
  - **Klasa:** `Flight`  
  - **Metoda:** `Flight.Create(...)` 

---

### 2. **Wyszukiwanie lotu po identyfikatorze**

- **Opis:** Możliwość wyszukania lotu po jego identyfikatorze (FlightId), do zaimplementowania przez warstwę infrastruktury.
- **Realizacja:**  
  - **Interfejs:** `IFlightRepository`  
  - **Metoda:** `GetFlightByFlightIdAsync(FlightId flightId)` 

---

### 3. **Zakup lotu po ID**

- **Opis:** Możliwość zakupu lotu wyszukanego po ID.
- **Realizacja:**  
  - **Klasa:** `PurchaseService`  
  - **Metoda:** `PurchaseFlight(PurchaseContext context)` 

---

### 4. **Zastosowanie zniżek do ceny lotu**

- **Realizacja:**  
  - **Interfejs:** `IDiscountCriteria`  
  - **Implementacje:**  
    - `BirthdayDiscount` (urodziny kupującego)  
    - `ThursdayAfricaDiscount` (lot do Afryki w czwartek)  
  - **Klasa:** `DiscountsApplier` – stosuje zniżki 

---

### 5. **Grupy tenantów i zapisywanie zniżek**

- **Realizacja:**  
  - **Klasa:** `TenantGroup` (A lub B)  
  - **Klasa:** `DiscountSavingPolicy` – decyduje, czy zapisać zniżki 

---

## Struktura domeny

- **Loty:**  
  - Klasa `Flight`
- **Tenanci:**  
  - Klasa `Tenant`
- **Kryteria zniżek:**  
  - Interfejs `IDiscountCriteria`
- **Zakup lotu:**  
  - Klasa `Purchase`

---

## Uwaga o warstwie aplikacyjnej

W pełnej architekturze DDD powyższe przypadki użycia byłyby realizowane przez **warstwę aplikacyjną** (komendy, zapytania).  
**Ten projekt skupia się tylko na modelu domenowym.**
