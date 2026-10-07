-- GymMasterAppDemo: complete reference catalog; Unicode codes and descriptions preserved
-- Run files in numbered order using SQL Server tooling (GO batch separator).
SET NOCOUNT ON;
USE [GymMasterDBDemo];
GO
IF DB_NAME() COLLATE Latin1_General_100_BIN2 <> N'GymMasterDBDemo'
    THROW 51000, 'Demo safety check failed. Expected GymMasterDBDemo.', 1;
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;
-- OccupationList: 5 reference records. Existing keys are preserved.
IF NOT EXISTS (SELECT 1 FROM [dbo].[OccupationList] WHERE [OccupationId] = N'1000')
    INSERT INTO [dbo].[OccupationList] ([OccupationId], [OccupationDesc])
    VALUES (N'1000', N'Φοιτητής/Μαθητής');
IF NOT EXISTS (SELECT 1 FROM [dbo].[OccupationList] WHERE [OccupationId] = N'1001')
    INSERT INTO [dbo].[OccupationList] ([OccupationId], [OccupationDesc])
    VALUES (N'1001', N'Δημόσιος Υπάλληλος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[OccupationList] WHERE [OccupationId] = N'1002')
    INSERT INTO [dbo].[OccupationList] ([OccupationId], [OccupationDesc])
    VALUES (N'1002', N'Ιδιωτικός Υπάλληλος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[OccupationList] WHERE [OccupationId] = N'1003')
    INSERT INTO [dbo].[OccupationList] ([OccupationId], [OccupationDesc])
    VALUES (N'1003', N'Ελεύθερος Επαγγελματίας');
IF NOT EXISTS (SELECT 1 FROM [dbo].[OccupationList] WHERE [OccupationId] = N'1004')
    INSERT INTO [dbo].[OccupationList] ([OccupationId], [OccupationDesc])
    VALUES (N'1004', N'Άνεργος');
-- ServicesList: 4 reference records. Existing keys are preserved.
IF NOT EXISTS (SELECT 1 FROM [dbo].[ServicesList] WHERE [ServiceCode] = N'100')
    INSERT INTO [dbo].[ServicesList] ([ServiceCode], [ServiceDescription])
    VALUES (N'100', N'Γενική Χρήση');
IF NOT EXISTS (SELECT 1 FROM [dbo].[ServicesList] WHERE [ServiceCode] = N'101')
    INSERT INTO [dbo].[ServicesList] ([ServiceCode], [ServiceDescription])
    VALUES (N'101', N'Όργανα/Βάρη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[ServicesList] WHERE [ServiceCode] = N'103')
    INSERT INTO [dbo].[ServicesList] ([ServiceCode], [ServiceDescription])
    VALUES (N'103', N'Ομαδικό πρόγραμμα');
IF NOT EXISTS (SELECT 1 FROM [dbo].[ServicesList] WHERE [ServiceCode] = N'104')
    INSERT INTO [dbo].[ServicesList] ([ServiceCode], [ServiceDescription])
    VALUES (N'104', N'Ατομικό πρόγραμμα/Personal');
-- MembershipTypeList: 4 reference records. Existing keys are preserved.
IF NOT EXISTS (SELECT 1 FROM [dbo].[MembershipTypeList] WHERE [MembershipType] = N'ΗΜ01')
    INSERT INTO [dbo].[MembershipTypeList] ([MembershipType], [Description], [DurationDays])
    VALUES (N'ΗΜ01', N'Ημερήσια', 1);
IF NOT EXISTS (SELECT 1 FROM [dbo].[MembershipTypeList] WHERE [MembershipType] = N'ΜΗ01')
    INSERT INTO [dbo].[MembershipTypeList] ([MembershipType], [Description], [DurationDays])
    VALUES (N'ΜΗ01', N'Μηνιαία', 30);
IF NOT EXISTS (SELECT 1 FROM [dbo].[MembershipTypeList] WHERE [MembershipType] = N'ΜΗ03')
    INSERT INTO [dbo].[MembershipTypeList] ([MembershipType], [Description], [DurationDays])
    VALUES (N'ΜΗ03', N'Τρίμηνη', 90);
IF NOT EXISTS (SELECT 1 FROM [dbo].[MembershipTypeList] WHERE [MembershipType] = N'ΜΗ12')
    INSERT INTO [dbo].[MembershipTypeList] ([MembershipType], [Description], [DurationDays])
    VALUES (N'ΜΗ12', N'Ετήσια', 365);
-- PayMethodList: 4 reference records. Existing keys are preserved.
IF NOT EXISTS (SELECT 1 FROM [dbo].[PayMethodList] WHERE [MethodType] = N'00')
    INSERT INTO [dbo].[PayMethodList] ([MethodType], [MethodDescription])
    VALUES (N'00', N'Μετρητά');
IF NOT EXISTS (SELECT 1 FROM [dbo].[PayMethodList] WHERE [MethodType] = N'10')
    INSERT INTO [dbo].[PayMethodList] ([MethodType], [MethodDescription])
    VALUES (N'10', N'Κάρτα/Prepaid');
IF NOT EXISTS (SELECT 1 FROM [dbo].[PayMethodList] WHERE [MethodType] = N'20')
    INSERT INTO [dbo].[PayMethodList] ([MethodType], [MethodDescription])
    VALUES (N'20', N'Κατάθεση WebBank');
IF NOT EXISTS (SELECT 1 FROM [dbo].[PayMethodList] WHERE [MethodType] = N'99')
    INSERT INTO [dbo].[PayMethodList] ([MethodType], [MethodDescription])
    VALUES (N'99', N'Άλλος τρόπος');
-- StatusList: 3 reference records. Existing keys are preserved.
IF NOT EXISTS (SELECT 1 FROM [dbo].[StatusList] WHERE [StatusCode] = N'0')
    INSERT INTO [dbo].[StatusList] ([StatusCode], [StatusDesc])
    VALUES (N'0', N'Ανεξόφλητη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[StatusList] WHERE [StatusCode] = N'1')
    INSERT INTO [dbo].[StatusList] ([StatusCode], [StatusDesc])
    VALUES (N'1', N'Εξοφλημένη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[StatusList] WHERE [StatusCode] = N'2')
    INSERT INTO [dbo].[StatusList] ([StatusCode], [StatusDesc])
    VALUES (N'2', N'Σε εκκρεμότητα');
-- WorkoutGoals: 5 reference records. Existing keys are preserved.
IF NOT EXISTS (SELECT 1 FROM [dbo].[WorkoutGoals] WHERE [GoalCode] = N'Π000')
    INSERT INTO [dbo].[WorkoutGoals] ([GoalCode], [Goal], [Description])
    VALUES (N'Π000', N'Γράμμωση', N'Εστίαση στην αύξηση της μυϊκής μάζας μέσω μέτριων επαναλήψεων (8-12) και σταδιακής αύξησης βάρους');
IF NOT EXISTS (SELECT 1 FROM [dbo].[WorkoutGoals] WHERE [GoalCode] = N'Π001')
    INSERT INTO [dbo].[WorkoutGoals] ([GoalCode], [Goal], [Description])
    VALUES (N'Π001', N'Όγκος', N'Στόχος η μείωση του σωματικού λίπους με διατήρηση της μυϊκής μάζας. Συνήθως συνδυάζεται με μικρότερα διαλείμματα και αερόβια άσκηση.');
IF NOT EXISTS (SELECT 1 FROM [dbo].[WorkoutGoals] WHERE [GoalCode] = N'Π002')
    INSERT INTO [dbo].[WorkoutGoals] ([GoalCode], [Goal], [Description])
    VALUES (N'Π002', N'Ενδυνάμωση', N'Προγράμματα που επικεντρώνονται στην αύξηση της μέγιστης δύναμης, χρησιμοποιώντας μεγάλα βάρη και λίγες επαναλήψεις (1-5).');
IF NOT EXISTS (SELECT 1 FROM [dbo].[WorkoutGoals] WHERE [GoalCode] = N'Π003')
    INSERT INTO [dbo].[WorkoutGoals] ([GoalCode], [Goal], [Description])
    VALUES (N'Π003', N'Βελτίωση Αντοχής', N'Εστίαση στην καρδιαγγειακή υγεία και τη μυϊκή αντοχή, με πολλές επαναλήψεις (15+) και αερόβια στοιχεία.');
IF NOT EXISTS (SELECT 1 FROM [dbo].[WorkoutGoals] WHERE [GoalCode] = N'Π004')
    INSERT INTO [dbo].[WorkoutGoals] ([GoalCode], [Goal], [Description])
    VALUES (N'Π004', N'Συντήρηση', N'Ένα ισορροπημένο πρόγραμμα για πελάτες που θέλουν απλώς να διατηρήσουν τη φυσική τους κατάσταση και την υγεία τους, χωρίς ακραίες αλλαγές στο σώμα τους.');
-- Exercises: 69 reference records. Existing keys are preserved.
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΔΚ10001')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΔΚ10001', N'Κάμψεις Δικεφάλων με Μπάρα (Barbell Curls)', N'Δικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΔΚ10002')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΔΚ10002', N'Κάμψεις Δικεφάλων με Αλτήρες', N'Δικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΔΚ10003')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΔΚ10003', N'Κάμψεις Σφυριά (Hammer Curls)', N'Δικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΔΚ10004')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΔΚ10004', N'Κάμψεις στο Μαξιλάρι (Preacher Curls)', N'Δικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΔΚ10005')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΔΚ10005', N'Κάμψεις Συγκέντρωσης (Concentration Curls)', N'Δικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΛ10001')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΛ10001', N'Ροκανίσματα (Crunches)', N'Κοιλιακοί');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΛ10002')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΛ10002', N'Ροκανίσματα στην Τροχαλία (Cable Crunches)', N'Κοιλιακοί');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΛ10003')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΛ10003', N'Άρσεις Ποδιών (Leg Raises)', N'Κοιλιακοί');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΛ10004')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΛ10004', N'Σανίδα (Plank)', N'Κοιλιακοί');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΛ10005')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΛ10005', N'Ρώσικες Περιστροφές (Russian Twists)', N'Κοιλιακοί');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΛ10006')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΛ10006', N'Ρόδα Κοιλιακών (Ab Wheel Rollout)', N'Κοιλιακοί');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΛ10007')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΛ10007', N'Ψαλιδάκια (Flutter Kicks)', N'Κοιλιακοί');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΛ10008')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΛ10008', N'Πλάγια Σανίδα (Side Plank)', N'Κοιλιακοί');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΜ10001')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΜ10001', N'Burpees', N'Ολόσωμες');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΜ10002')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΜ10002', N'Αιωρήσεις με Kettlebell (Kettlebell Swings)', N'Ολόσωμες');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΜ10003')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΜ10003', N'Mountain Climbers', N'Ολόσωμες');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΚΜ10004')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΚΜ10004', N'Καθίσματα με Άλμα (Jump Squats)', N'Ολόσωμες');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10001')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10001', N'Καθίσματα με Μπάρα (Back Squats)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10002')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10002', N'Καθίσματα μπροστά (Front Squats)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10003')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10003', N'Καθίσματα με Αλτήρα (Goblet Squats)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10004')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10004', N'Πιέσεις Ποδιών (Leg Press)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10005')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10005', N'Προβολές (Lunges)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10006')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10006', N'Βουλγαρικά Καθίσματα (Bulgarian Split Squats)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10007')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10007', N'Εκτάσεις Τετρακέφαλων (Leg Extensions)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10008')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10008', N'Κάμψεις Οπίσθιων Μηριαίων (Leg Curls)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10009')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10009', N'Ρουμάνικες Άρσεις Θανάτου (RDLs)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10010')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10010', N'Ανυψώσεις Λεκάνης (Hip Thrusts)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10011')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10011', N'Απαγωγοί στο Μηχάνημα', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10012')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10012', N'Προσαγωγοί στο Μηχάνημα', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10013')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10013', N'Ακροστασίες για Γάμπες Όρθιος (Standing Calf Raises)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΔ10014')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΔ10014', N'Ακροστασίες για Γάμπες Καθιστός (Seated Calf Raises)', N'Πόδια');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΛ10001')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΛ10001', N'Έλξεις στο Μονόζυγο (Pull-ups)', N'Πλάτη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΛ10002')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΛ10002', N'Έλξεις με Ανάποδη Λαβή (Chin-ups)', N'Πλάτη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΛ10003')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΛ10003', N'Έλξεις στην Τροχαλία (Lat Pulldown)', N'Πλάτη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΛ10004')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΛ10004', N'Έλξεις Τροχαλίας με Κλειστή Λαβή (V-Bar Pulldown)', N'Πλάτη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΛ10005')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΛ10005', N'Κωπηλατική με Μπάρα (Barbell Row)', N'Πλάτη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΛ10006')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΛ10006', N'Κωπηλατική με Αλτήρα (Dumbbell Row)', N'Πλάτη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΛ10007')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΛ10007', N'Κωπηλατική στην Τροχαλία (Seated Cable Row)', N'Πλάτη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΛ10008')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΛ10008', N'Κωπηλατική T-Bar', N'Πλάτη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΛ10009')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΛ10009', N'Άρσεις Θανάτου (Deadlifts)', N'Πλάτη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΛ10010')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΛ10010', N'Ραχιαίοι στο Μηχάνημα (Hyperextensions)', N'Πλάτη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΠΛ10011')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΠΛ10011', N'Πιέσεις Τροχαλίας με Τεντωμένα Χέρια (Straight Arm Pulldown)', N'Πλάτη');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΣΤ10001')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΣΤ10001', N'Πιέσεις Πάγκου με Μπάρα (Επίπεδος)', N'Στήθος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΣΤ10002')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΣΤ10002', N'Πιέσεις Πάγκου με Αλτήρες (Επίπεδος)', N'Στήθος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΣΤ10003')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΣΤ10003', N'Επικλινείς Πιέσεις με Μπάρα', N'Στήθος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΣΤ10004')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΣΤ10004', N'Επικλινείς Πιέσεις με Αλτήρες', N'Στήθος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΣΤ10005')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΣΤ10005', N'Κατακλινείς Πιέσεις', N'Στήθος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΣΤ10006')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΣΤ10006', N'Εκτάσεις Στήθους με Αλτήρες (Flyes)', N'Στήθος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΣΤ10007')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΣΤ10007', N'Εκτάσεις Στήθους στο Μηχάνημα (Pec Deck)', N'Στήθος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΣΤ10008')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΣΤ10008', N'Διασταυρώσεις στην Τροχαλία (Cable Crossovers)', N'Στήθος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΣΤ10009')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΣΤ10009', N'Βυθίσεις στο Δίζυγο (Dips - Έμφαση Στήθος)', N'Στήθος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΣΤ10010')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΣΤ10010', N'Κάμψεις (Push-ups)', N'Στήθος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΣΤ10011')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΣΤ10011', N'Pull-over με Αλτήρα', N'Στήθος');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΤΡ10001')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΤΡ10001', N'Κάμψεις στην Τροχαλία με Σχοινί', N'Τρικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΤΡ10002')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΤΡ10002', N'Πιέσεις Τρικεφάλων στην Τροχαλία (Pushdowns)', N'Τρικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΤΡ10003')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΤΡ10003', N'Γαλλικές Πιέσεις (Skull Crushers)', N'Τρικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΤΡ10004')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΤΡ10004', N'Εκτάσεις Τρικεφάλων πάνω από το Κεφάλι', N'Τρικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΤΡ10005')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΤΡ10005', N'Πιέσεις Πάγκου με Κλειστή Λαβή (Close-Grip Bench Press)', N'Τρικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΤΡ10006')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΤΡ10006', N'Λακτίσματα με Αλτήρα (Triceps Kickbacks)', N'Τρικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΤΡ10007')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΤΡ10007', N'Βυθίσεις σε Πάγκο (Bench Dips)', N'Τρικέφαλοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΩΜ10001')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΩΜ10001', N'Στρατιωτικές Πιέσεις με Μπάρα (Overhead Press)', N'Ώμοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΩΜ10002')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΩΜ10002', N'Πιέσεις Ώμων με Αλτήρες (Dumbbell Press)', N'Ώμοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΩΜ10003')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΩΜ10003', N'Πλάγιες Εκτάσεις με Αλτήρες (Lateral Raises)', N'Ώμοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΩΜ10004')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΩΜ10004', N'Πλάγιες Εκτάσεις στην Τροχαλία', N'Ώμοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΩΜ10005')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΩΜ10005', N'Προτάσεις Ώμων (Front Raises)', N'Ώμοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΩΜ10006')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΩΜ10006', N'Εκτάσεις Οπίσθιων Δελτοειδών (Reverse Pec Deck)', N'Ώμοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΩΜ10007')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΩΜ10007', N'Έλξεις με Σχοινί στο Πρόσωπο (Face Pulls)', N'Ώμοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΩΜ10008')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΩΜ10008', N'Όρθια Κωπηλατική (Upright Row)', N'Ώμοι');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Exercises] WHERE [ExerciseCode] = N'ΩΜ10009')
    INSERT INTO [dbo].[Exercises] ([ExerciseCode], [ExerciseName], [MuscleGroup])
    VALUES (N'ΩΜ10009', N'Ανασηκώσεις Ώμων (Shrugs)', N'Ώμοι');
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
