
USE JANGOBD 
CREATE PROCEDURE InserirDividaDePropina
BEGIN
   DECLARE PropinaNaoPago,Mes INT;
     DECLARE class1,class2, class3,class4,class5,class6,class7 INT;
     DECLARE class8,class9,class10,class11 ,class12,class13,class14 INT;
	 DECLARE contaMes INT default 0;
DECLARE MesesPago INT default 3;
DECLARE LimiteClasseExame,LimiteClasseTransicao INT;
DECLARE CodClasse,BuscaClasseExame,IdAluno INT;
DECLARE BuscaIdClasse,BuscaIdTurma INT;
DECLARE BuscaPrecoPropina DECIMAL(9,2);
DECLARE BuscaAnoLetivo VARCHAR(14);


set class1 = 18;set class2 = 1;set class3 = 2;set class4 = 3;set class5 = 4;
set class6 = 5;set class7 = 6;set class8 = 7;set class9 = 8;set class10 = 9;
set class11 = 10;set class12 = 11;set class13 = 12;set class14 = 13;
set LimiteClasseExame = 11;set LimiteClasseTransicao = 10;
 
/*Buscar o último aluno matriculado (que foi inserido)*/
set IdAluno = ( select MAX(Id) from AlunoMatriculados);
   /* Buscar o Id da classe do último aluno matriculado*/
  set BuscaIdClasse = (SELECT
    ClasseId
    FROM AlunoMatriculados 
 where Id = (IdAluno));
  /*Buscar o Id da Turma do último aluno matriculado*/
set BuscaIdTurma = (SELECT
     TurmaId
    FROM AlunoMatriculados 
 where Id = (IdAluno));
 
 /*Saber se é classe de exame ou não do último aluno matriculado*/
 set BuscaClasseExame = (SELECT
    cl.ClassDeExame
    FROM AlunoMatriculados almat
    inner join Classes cl 
    on almat.ClasseId = cl.Id
 where Id = (IdAluno));

 /*Busca o preço da propina em função da classe do ultimo aluno matriculado*/
 set BuscaPrecoPropina = (SELECT
    cl.PrecoPropina FROM AlunoMatriculados almat
   inner join Classes cl
    on almat.ClasseId = cl.Id
 where Id = (IdAluno));

 /*Busca o Ano Letivo do último aluno matriculado*/
 set BuscaAnoLetivo = (SELECT AnoLetivo
    FROM 
    AlunoMatriculados where Id = (IdAluno));
/*Busca o Id da classe do último aluno matriculado*/
 set CodClasse = (SELECT
     ClasseId FROM AlunoMatriculados
   where Id =  IdAluno);
   
 CASE
	 WHEN class1 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  Propinas
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
     
     WHEN class2 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
	 
     WHEN class3 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
     
     WHEN class4 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	  while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
     
     WHEN class5 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	  while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
     
     WHEN class6 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	  while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	  while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
	 
     WHEN class7 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	  while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	   while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
     
     WHEN class8 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	  while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	  while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
     
     WHEN class9 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
	 
     WHEN class10 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
     
     WHEN class11 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	  while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
     
     WHEN class12 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	  while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	    while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
     
     WHEN class13 = CodClasse THEN
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
  END WHILE;
 END IF;
	
    ELSE
     IF BuscaClasseExame = 1 THEN
  WHILE contaMes < LimiteClasseExame DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo);
   END WHILE; 
 
 ELSE
     WHILE contaMes < LimiteClasseTransicao DO
	while contaMes < MesesPago  Do
    SET contaMes =  contaMes + 1;
       /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,1,BuscaAnoLetivo);
    end while ;

	SET contaMes =  contaMes + 1;
     /*inserir dívidas de propinas ao último aluno matriculado que foi inserido*/
  insert into 
  tbpropina
  (idMes,idAlunoMatriculado,idClasse,
idTurma,DataCobranca,PrecoPropina,Estado,AnoLetivo)
  values(contaMes,IdAluno,BuscaIdClasse,BuscaIdTurma
,CURDATE(),BuscaPrecoPropina,0,BuscaAnoLetivo); 
  END WHILE;
 END IF;
    
END CASE;

END 