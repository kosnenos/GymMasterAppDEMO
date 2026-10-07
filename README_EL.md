# GymMaster

[English](README.md) | [Ελληνικά](README_EL.md)

**Σύστημα διαχείρισης γυμναστηρίου για Windows — Έκδοση Portfolio / Demo**

Ολοκληρωμένη desktop εφαρμογή για τη διαχείριση πελατών, συνδρομών, πληρωμών, ιστορικού υγείας και εξατομικευμένων προγραμμάτων προπόνησης.

Αναπτύχθηκε με **C#**, **Windows Forms**, **SQL Server**, **ADO.NET** και το αρχιτεκτονικό πρότυπο **Model–View–Presenter (MVP)**.

## Επισκόπηση

Το GymMasterAppDemo είναι μια εφαρμογή portfolio για Windows που υποστηρίζει την οργάνωση πελατών, συνδρομών, πληρωμών και ατομικών προγραμμάτων άθλησης. Περιλαμβάνει dashboard, αναφορές και εργαλεία εκτύπωσης, με αποκλειστική βάση δεδομένων demo στον SQL Server.

Το περιβάλλον της εφαρμογής είναι κυρίως στα Ελληνικά. Τα SQL scripts που παρέχονται αναπαράγουν το schema, τους καταλόγους αναφοράς και τις συνθετικές εγγραφές επίδειξης.

## Βασικές λειτουργίες

- Διαχείριση και αναζήτηση πελατών.
- Ιστορικό υγείας και στοιχεία επικοινωνίας έκτακτης ανάγκης.
- Διαχείριση συνδρομών, ημερομηνιών, κατάστασης και τιμολόγησης.
- Παρακολούθηση πληρωμών και ιστορικό κινήσεων.
- Dashboard με γραφήματα και συνοπτικούς δείκτες.
- Αναφορές για νέες εγγραφές, συνδρομές που λήγουν σήμερα και ανεξόφλητες συνδρομές.
- Ατομικά προγράμματα προπόνησης, στόχοι και αναλυτικά στοιχεία ασκήσεων.
- Εκτύπωση και προεπισκόπηση εκτύπωσης συνδρομών και προγραμμάτων.
- Εξαγωγή αναλυτικών αναφορών σε Excel μέσω ClosedXML.
- Δημιουργία αντιγράφου ασφαλείας αποκλειστικά της demo βάσης.

## Στιγμιότυπα οθόνης

Τα παρακάτω στιγμιότυπα προέρχονται από την εφαρμογή σε λειτουργία με συνθετικά demo δεδομένα.

### Dashboard

Συνοπτικοί δείκτες και γραφήματα για συνδρομές, έσοδα και δημογραφικά στοιχεία πελατών.

![Dashboard του GymMaster](screenshots/dashboard.png)

### Διαχείριση πελατών

Αναζήτηση και προβολή πελατών με συνθετικά στοιχεία επικοινωνίας και προφίλ.

![Διαχείριση πελατών στο GymMaster](screenshots/customers.png)

### Συνδρομές και πληρωμές

Στοιχεία συνδρομών, ιστορικό πληρωμών και ανεξόφλητα υπόλοιπα.

![Συνδρομές και πληρωμές στο GymMaster](screenshots/memberships.png)

### Αναλυτικές αναφορές

Αναφορές νέων εγγραφών, συνδρομών που λήγουν και ανεξόφλητων συνδρομών.

![Αναλυτικές αναφορές του GymMaster](screenshots/analytics.png)

### Προγράμματα προπόνησης

Ρύθμιση προγράμματος με στόχους, ημερομηνίες, συχνότητα και αναλυτικά στοιχεία ασκήσεων.

![Προγράμματα προπόνησης στο GymMaster](screenshots/workout-programs.png)

## Τεχνικά χαρακτηριστικά

- Διαχωρισμός του UI από τη λογική της εφαρμογής μέσω MVP.
- Πρόσβαση στον SQL Server μέσω repositories και ADO.NET.
- Σχεσιακή βάση με 13 πίνακες, foreign keys, constraints και indexes.
- Αναπαραγώγιμη εγκατάσταση της βάσης μέσω SQL scripts.
- Συνθετικό demo dataset με δυναμικές ημερομηνίες.
- Runtime safety guard που αποτρέπει συνδέσεις σε βάση εκτός της επιτρεπόμενης demo βάσης.
- Εξαγωγή σε Excel μέσω ClosedXML.
- Υποστήριξη εκτύπωσης και προεπισκόπησης εκτύπωσης.
- Γραφήματα dashboard μέσω Windows Forms DataVisualization.

## Αρχιτεκτονική

Η εφαρμογή ακολουθεί το πρότυπο Model–View–Presenter (MVP):

- **Models**: αναπαριστούν τα δεδομένα πελατών, υγείας, συνδρομών, πληρωμών και προγραμμάτων.
- **Views**: ορίζουν τα interfaces που υλοποιούν οι Windows Forms.
- **Presenters**: συντονίζουν τα συμβάντα των views και τις λειτουργίες της εφαρμογής.
- **Data**: περιλαμβάνει ADO.NET repositories, database configuration και τον μηχανισμό προστασίας σύνδεσης της demo έκδοσης.
- **Services**: παρέχει τη λειτουργία backup της βάσης.
- **Printing**: δημιουργεί τα έγγραφα εκτύπωσης συνδρομών και προγραμμάτων προπόνησης.

Το solution περιλαμβάνει ένα desktop project. Τα SQL deployment scripts διατηρούνται σε ξεχωριστό φάκελο.

## Τεχνολογίες

- C# και Windows Forms.
- .NET Framework 4.8.
- SQL Server Express και Windows Authentication.
- ADO.NET / `System.Data.SqlClient`.
- ClosedXML και εξαρτήσεις Open XML για την εξαγωγή σε Excel.
- Γραφήματα Windows Forms DataVisualization.
- Διαχείριση των NuGet dependencies μέσω `packages.config`.

## Βάση δεδομένων

Η εφαρμογή χρησιμοποιεί τη **GymMasterDBDemo**. Το schema περιλαμβάνει 13 πίνακες για πελάτες, ιστορικό υγείας, συνδρομές, πληρωμές, προγράμματα προπόνησης και καταλόγους αναφοράς.

Τα deployment scripts βρίσκονται στον φάκελο `database/` και εκτελούνται με την ακόλουθη σειρά:

1. `01_CreateDatabase.sql`
2. `02_CreateSchema.sql`
3. `03_SeedReferenceData.sql`
4. `04_SeedDemoData.sql`

Το script δημιουργίας δεν τροποποιεί μια βάση που υπάρχει ήδη. Το script του schema προϋποθέτει ότι οι πίνακες της εφαρμογής δεν υπάρχουν και σταματά εάν τους εντοπίσει. Το reference seed διατηρεί τα υφιστάμενα keys. Το demo seed **αντικαθιστά τα transactional δεδομένα της GymMasterDBDemo** και ελέγχει το όνομα της ενεργής βάσης πριν από τις αλλαγές. Χρησιμοποιήστε αποκλειστική βάση για το demo.

Ο SQL Server δημιουργεί τα identity IDs και το seed διατηρεί τις σχέσεις μέσω ID maps. Οι ημερομηνίες συνδρομών, πληρωμών, πρόσφατων εγγραφών και προγραμμάτων υπολογίζονται σε σχέση με την ημέρα εκτέλεσης, ώστε το dashboard και οι αναφορές να παραμένουν χρήσιμα. Οι ημερομηνίες γέννησης είναι σταθερές συνθετικές τιμές.

## Demo δεδομένα

| Πίνακας | Εγγραφές |
|---|---:|
| Customers | 25 |
| HealthRecord | 12 |
| Membership | 20 |
| Payments | 23 |
| ProgramsWorkout | 8 |
| ProgramsWorkoutDetails | 23 |

Τα customer IDs είναι **10001 έως 10025**. Ένας πελάτης είναι ανενεργός, οπότε οι υπάρχουσες λίστες ενεργών πελατών εμφανίζουν 24 πελάτες.

Τα reference δεδομένα περιλαμβάνουν 5 επαγγέλματα, 4 υπηρεσίες, 4 τύπους συνδρομής, 4 τρόπους πληρωμής, 3 καταστάσεις συνδρομής, 5 στόχους προπόνησης και ολόκληρο τον κατάλογο των **69 ασκήσεων**. Οι κωδικοί συνδρομών `ΗΜ01`, `ΜΗ01`, `ΜΗ03` και `ΜΗ12` χρησιμοποιούν ελληνικούς χαρακτήρες.

Όλες οι εγγραφές που σχετίζονται με πελάτες είναι συνθετικά/demo δεδομένα.

## Εγκατάσταση / Ρύθμιση

### Προϋποθέσεις

- Windows.
- Visual Studio 2022 με το workload **.NET desktop development** και το **.NET Framework 4.8 targeting pack**.
- SQL Server Express, με προεπιλεγμένο instance το `.\SQLEXPRESS`.
- Windows Authentication και δικαιώματα δημιουργίας της demo βάσης και των πινάκων της.
- SQL Server Management Studio ή άλλος SQL client που υποστηρίζει τον διαχωρισμό batches με `GO`.
- Πρόσβαση στο NuGet για restore των καθορισμένων εκδόσεων των packages.

### Εγκατάσταση βάσης

Συνδεθείτε στο `.\SQLEXPRESS` με Windows Authentication και εκτελέστε τα τέσσερα scripts με τη σειρά που αναφέρεται παραπάνω. Διατηρήστε την αρχική Unicode κωδικοποίηση των SQL αρχείων.

### Ρύθμιση solution

1. Ανοίξτε το `GymMasterAppDemo.sln` στο Visual Studio.
2. Εκτελέστε **Restore NuGet Packages** στο solution.
3. Ορίστε το `GymMasterAppDemo` ως startup project.
4. Επιλέξτε **Debug / Any CPU** και εκτελέστε **Rebuild Solution**.

Ο τοπικός φάκελος `packages/` εξαιρείται από το Git. Το `packages.config` και τα project references περιλαμβάνονται, ώστε να γίνεται restore των ίδιων ακριβώς εκδόσεων. Δεν απαιτείται αλλαγή των package versions.

### Configuration

Το `GymMasterAppDemo/App.config` ορίζει το `GymMasterDbConnection` με το ακόλουθο προεπιλεγμένο connection string:

```text
Data Source=.\SQLEXPRESS;Initial Catalog=GymMasterDBDemo;Integrated Security=True;TrustServerCertificate=True
```

Εάν χρησιμοποιείτε άλλο SQL Server instance, αλλάξτε το `Data Source` στο `App.config`. Διατηρήστε το `Initial Catalog=GymMasterDBDemo` και χρησιμοποιήστε Windows Authentication.

Ο safety guard της demo έκδοσης επιτρέπει σύνδεση μόνο σε βάση με όνομα **GymMasterDBDemo**. Ελέγχει τόσο το configured catalog όσο και τη βάση της ενεργής σύνδεσης. Δεν υπάρχει fallback σε εναλλακτική βάση.

## Εκτέλεση της εφαρμογής

Μετά την εγκατάσταση της βάσης και το package restore, εκκινήστε την εφαρμογή με **F5** ή **Ctrl+F5** στο Visual Studio. Το κύριο παράθυρο ανοίγει το dashboard και εμφανίζει ένδειξη σύνδεσης με τη βάση.

Από το μενού πλοήγησης μπορείτε να προβάλετε πελάτες, συνδρομές, αναφορές και προγράμματα προπόνησης. Οι αναφορές και τα γραφήματα εξαρτώνται από τις ημερομηνίες του seed. Εκτελέστε ξανά το demo seed όταν θέλετε να ανανεώσετε τα σενάρια επίδειξης.

Η λειτουργία backup αφορά μόνο τη GymMasterDBDemo και αποθηκεύει τα αρχεία στον φάκελο `C:\GymMasterAppDemo_Backups`. Ο λογαριασμός υπηρεσίας του SQL Server πρέπει να έχει δικαίωμα εγγραφής στον φάκελο και το Windows login δικαίωμα backup της βάσης. Η διαδρομή βρίσκεται στο μηχάνημα του SQL Server.

## Δομή έργου

```text
GymMasterAppDemo/
├── GymMasterAppDemo.sln
├── GymMasterAppDemo/
│   ├── Data/
│   ├── Forms/
│   ├── Models/
│   ├── Presenters/
│   ├── Printing/
│   ├── Properties/
│   ├── Resources/
│   ├── Services/
│   ├── Views/
│   ├── App.config
│   ├── GymMasterAppDemo.csproj
│   └── packages.config
├── database/
│   ├── 01_CreateDatabase.sql
│   ├── 02_CreateSchema.sql
│   ├── 03_SeedReferenceData.sql
│   └── 04_SeedDemoData.sql
├── screenshots/
├── .gitignore
├── README.md
└── README_EL.md
```

## Υφιστάμενοι περιορισμοί

- Desktop εφαρμογή .NET Framework που εκτελείται μόνο σε Windows.
- Ορισμένα queries ειδικά για το dashboard παραμένουν συνδεδεμένα με τη φόρμα Dashboard και μπορούν να μεταφερθούν σε ξεχωριστά repositories σε μελλοντικό refactoring.
- Η προεπιλεγμένη demo configuration χρησιμοποιεί SQL Server Express με Windows Authentication.

## Δήλωση απορρήτου & Demo έκδοσης

Το repository αποτελεί έκδοση portfolio/demo. Όλες οι εγγραφές πελατών, ιστορικού υγείας, συνδρομών, πληρωμών και προγραμμάτων προπόνησης που περιλαμβάνονται στη demo βάση είναι συνθετικές και δεν αντιπροσωπεύουν πραγματικά άτομα.

Η demo έκδοση προορίζεται για παρουσίαση της εφαρμογής. Διατηρήστε τη σύνδεσή της με την αποκλειστική βάση συνθετικών δεδομένων. Τα τοπικά IDE metadata, build outputs, restored packages, logs και binary αρχεία δεδομένων/backup του SQL Server εξαιρούνται από το Git.
