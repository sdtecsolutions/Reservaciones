CREATE DATABASE Reservacion
GO
USE [Reservacion]
GO
/****** Object:  StoredProcedure [dbo].[ACT_HORARIO]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ACT_HORARIO]
    (
      @COD_HORA INT ,
      @HOR_INIC CHAR(5) ,
      @HOR_FINA CHAR(5)
    )
AS
    BEGIN
        UPDATE  HORARIO
        SET     HOR_INIC = @HOR_INIC ,
                HOR_FINA = @HOR_FINA
        WHERE   COD_HORA = @COD_HORA
    END
GO
/****** Object:  StoredProcedure [dbo].[ACT_TIPOCANCHA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ACT_TIPOCANCHA]
    (
      @COD_TIPO_CANC INT ,
      @ALF_TIPO_CANC VARCHAR(100) ,
      @COD_TIPO_DEPO INT ,
      @NUM_JUGA INT ,
      @MON_PREC NUMERIC(10, 2)
    )
AS
    BEGIN
        UPDATE  TIPO_CANCHA
        SET     ALF_TIPO_CANC = @ALF_TIPO_CANC ,
                COD_TIPO_DEPO = @COD_TIPO_DEPO ,
                NUM_JUGA = @NUM_JUGA ,
                MON_PREC = @MON_PREC
        WHERE   COD_TIPO_CANC = @COD_TIPO_CANC    
    END

GO
/****** Object:  StoredProcedure [dbo].[ACT_TIPODEPORTE]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ACT_TIPODEPORTE]
    (
      @COD_TIPO_DEPO INT ,
      @ALF_TIPO_DEPO VARCHAR(100) 
    )
AS
    BEGIN
		  UPDATE TIPO_DEPORTE  SET ALF_TIPO_DEPO=@ALF_TIPO_DEPO
          WHERE  COD_TIPO_DEPO=@COD_TIPO_DEPO      
	END

GO
/****** Object:  StoredProcedure [dbo].[DEL_HORARIO]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_HORARIO] ( @COD_HORA INT )
AS
    BEGIN	
        DELETE  FROM HORARIO
        WHERE   COD_HORA = @COD_HORA     
    END

GO
/****** Object:  StoredProcedure [dbo].[DEL_TIPOCANCHA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_TIPOCANCHA] ( @COD_TIPO_CANC INT )
AS
    BEGIN	
        DELETE  FROM TIPO_CANCHA
        WHERE   COD_TIPO_CANC = @COD_TIPO_CANC      
    END

GO
/****** Object:  StoredProcedure [dbo].[DEL_TIPODEPORTE]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_TIPODEPORTE] ( @COD_TIPO_DEPO INT )
AS
    BEGIN	
        DELETE  FROM TIPO_DEPORTE
        WHERE   COD_TIPO_DEPO = @COD_TIPO_DEPO      
    END

GO
/****** Object:  StoredProcedure [dbo].[INS_CLIENTE]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INS_CLIENTE]
    (
      @COD_CLIE INT OUTPUT ,
      @ALF_NOMB VARCHAR(200) ,
      @ALF_TIPO_DOCU VARCHAR(10) ,
      @ALF_NUME_DOCU VARCHAR(15) ,
      @ALF_CORR VARCHAR(50) ,
      @ALF_NUME_TELE VARCHAR(30)
    )
AS
    BEGIN
        SET @COD_CLIE = ( SELECT    c.COD_CLIE
                          FROM      CLIENTE AS c
                          WHERE     RTRIM(LTRIM(c.ALF_NUME_DOCU)) = RTRIM(LTRIM(@ALF_NUME_DOCU))
                        )
	            
        IF @COD_CLIE IS NULL
            BEGIN
                SET @COD_CLIE = ( SELECT    ISNULL(MAX(c.COD_CLIE), 0)
                                  FROM      CLIENTE AS c
                                ) + 1
	    
                INSERT  INTO CLIENTE
                        ( COD_CLIE ,
                          ALF_NOMB ,
                          ALF_TIPO_DOCU ,
                          ALF_NUME_DOCU ,
                          ALF_CORR ,
                          ALF_NUME_TELE
	                    )
                VALUES  ( @COD_CLIE ,
                          @ALF_NOMB ,
                          @ALF_TIPO_DOCU ,
                          @ALF_NUME_DOCU ,
                          @ALF_CORR ,
                          @ALF_NUME_TELE
	                    )
            END
        ELSE
            BEGIN
                UPDATE  CLIENTE
                SET     ALF_NOMB = @ALF_NOMB ,
                        ALF_TIPO_DOCU = @ALF_TIPO_DOCU ,
                        ALF_NUME_DOCU = @ALF_NUME_DOCU ,
                        ALF_CORR = @ALF_CORR ,
                        ALF_NUME_TELE = @ALF_NUME_TELE
                WHERE   COD_CLIE = @COD_CLIE
            END
    END
GO
/****** Object:  StoredProcedure [dbo].[INS_HORARIO]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INS_HORARIO]
    (
      @COD_HORA INT ,
      @HOR_INIC CHAR(5) ,
      @HOR_FINA CHAR(5)
    )
AS
    BEGIN
        SET @COD_HORA = ( SELECT    ISNULL(MAX(h.COD_HORA), 0) + 1
                          FROM      HORARIO AS h
                        )
        INSERT  INTO HORARIO
                ( COD_HORA, HOR_INIC, HOR_FINA )
        VALUES  ( @COD_HORA, @HOR_INIC, @HOR_FINA )
    END

GO
/****** Object:  StoredProcedure [dbo].[INS_PEDIDO]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INS_PEDIDO]
    (
      @ALF_NUME_PEDI VARCHAR(10) OUTPUT ,
      @COD_CLIE INT ,
      @COD_TIPO_DEPO INT ,
      @COD_TIPO_CANC INT ,
      @COD_HORA INT ,
      @FEC_HORA_RESE SMALLDATETIME
    )
AS
    BEGIN
        DECLARE @IsValid INT = ( SELECT COUNT(p.COD_PEDI)
                                 FROM   PEDIDO AS p
                                        INNER JOIN dbo.HORARIO AS h ON h.COD_HORA = p.COD_HORA
                                 WHERE  p.COD_TIPO_DEPO = @COD_TIPO_DEPO
                                        AND p.COD_TIPO_CANC = @COD_TIPO_CANC
                                        AND p.COD_HORA = @COD_HORA
                                        AND CONVERT(VARCHAR, p.FEC_HORA_RESE, 12) = CONVERT(VARCHAR, @FEC_HORA_RESE, 12)
                                        AND CAST(REPLACE(h.HOR_FINA, ':', '') AS INT) <= CAST(REPLACE(LEFT(CONVERT(VARCHAR, GETDATE(), 108),
                                                              5), ':', '') AS INT)
                               )
	
        IF @IsValid = 1
            BEGIN
                RAISERROR('El espacio deportivo ya está reservado en esta fecha y hora.', 13, 1)
                RETURN
            END
	
        SET @IsValid = ( SELECT COUNT(p.COD_PEDI)
                         FROM   PEDIDO AS p
                         WHERE  p.COD_CLIE = @COD_CLIE
                                AND p.COD_TIPO_DEPO = @COD_TIPO_DEPO
                                AND p.COD_TIPO_CANC = @COD_TIPO_CANC
                                AND p.COD_HORA = @COD_HORA
                                AND CONVERT(VARCHAR, p.FEC_HORA_RESE, 12) = CONVERT(VARCHAR, @FEC_HORA_RESE, 12)
                       )
	
        IF @IsValid = 1
            BEGIN
                RAISERROR('Ud. ya tiene una reservación del espacio deportivo en esta fecha y hora.', 13, 1)
                RETURN
            END
	
        DECLARE @COD_PEDI INT = ( SELECT    ISNULL(MAX(p.COD_PEDI), 0) + 1
                                  FROM      PEDIDO AS p
                                )
	
        SET @ALF_NUME_PEDI = ( SELECT   'P' + RIGHT('0000'
                                                    + CAST(( ISNULL(MAX(CAST(RIGHT(p.ALF_NUME_PEDI,
                                                              4) AS INT)), 0)
                                                             + 1 ) AS VARCHAR),
                                                    4)
                               FROM     PEDIDO AS p
                             )
	
        INSERT  INTO PEDIDO
                ( COD_PEDI ,
                  ALF_NUME_PEDI ,
                  COD_CLIE ,
                  COD_TIPO_DEPO ,
                  COD_TIPO_CANC ,
                  FEC_HORA_RESE ,
                  COD_HORA ,
                  IND_ESTA
	            )
        VALUES  ( @COD_PEDI ,
                  @ALF_NUME_PEDI ,
                  @COD_CLIE ,
                  @COD_TIPO_DEPO ,
                  @COD_TIPO_CANC ,
                  @FEC_HORA_RESE ,
                  @COD_HORA ,
                  'P'
	            )
    END



GO
/****** Object:  StoredProcedure [dbo].[INS_RESERVA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INS_RESERVA]
    (
       @COD_RESE INT OUTPUT,
       @COD_PEDI INT,
       @MON_PAGA NUMERIC(10, 2),
       @MON_PAGO NUMERIC(10, 2)
    )
AS
    BEGIN
        DECLARE @SUM_PAGO INT = ISNULL(( SELECT r.MON_PAGO
                                 FROM RESERVA AS r
                                 WHERE r.COD_PEDI = @COD_PEDI
                               ), 0.0) + @MON_PAGO
	
        IF @MON_PAGA < @SUM_PAGO
            BEGIN
                RAISERROR('El monto pagado supera la deuda.', 13, 1)
                RETURN
            END
	
		DECLARE @IND_ESTA CHAR(1)
		IF  @MON_PAGA = @SUM_PAGO
		BEGIN
			SET @IND_ESTA = 'C'
			UPDATE PEDIDO
			SET
			    IND_ESTA = @IND_ESTA
			WHERE COD_PEDI = @COD_PEDI
		END
		ELSE
			BEGIN
				SET @IND_ESTA = 'P'
			END		
		
        SET @COD_RESE = ( SELECT    ISNULL(MAX(r.COD_RESE), 0) + 1
                                  FROM RESERVA AS r
                                )
	
        INSERT INTO RESERVA
        (
        	COD_RESE,
        	COD_PEDI,
        	MON_PAGA,
        	MON_PAGO,
        	IND_ESTA
        )
        VALUES
        (
        	@COD_RESE,
        	@COD_PEDI,
        	@MON_PAGA,
        	@MON_PAGO,
        	@IND_ESTA
        )
    END

GO
/****** Object:  StoredProcedure [dbo].[INS_TIPOCANCHA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INS_TIPOCANCHA]
    (
      @COD_TIPO_CANC INT ,
      @ALF_TIPO_CANC VARCHAR(100) ,
      @COD_TIPO_DEPO INT ,
      @NUM_JUGA INT ,
      @MON_PREC NUMERIC(10, 2)
    )
AS
    BEGIN
        SET @COD_TIPO_CANC = ( SELECT   ISNULL(MAX(tc.COD_TIPO_CANC), 0) + 1
                               FROM     TIPO_CANCHA AS tc
                             )
        INSERT  INTO TIPO_CANCHA
                ( COD_TIPO_CANC ,
                  ALF_TIPO_CANC ,
                  COD_TIPO_DEPO ,
                  NUM_JUGA ,
                  MON_PREC
                )
        VALUES  ( @COD_TIPO_CANC ,
                  @ALF_TIPO_CANC ,
                  @COD_TIPO_DEPO ,
                  @NUM_JUGA ,
                  @MON_PREC
                )
    END

GO
/****** Object:  StoredProcedure [dbo].[INS_TIPODEPORTE]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INS_TIPODEPORTE]
    (
      @COD_TIPO_DEPO INT ,
      @ALF_TIPO_DEPO VARCHAR(100)
    )
AS
    BEGIN
        SET @COD_TIPO_DEPO = ( SELECT   ISNULL(MAX(td.COD_TIPO_DEPO), 0) + 1
                               FROM     TIPO_DEPORTE AS td
                             )
        INSERT  INTO TIPO_DEPORTE
                ( COD_TIPO_DEPO, ALF_TIPO_DEPO )
        VALUES  ( @COD_TIPO_DEPO, @ALF_TIPO_DEPO )
    END

GO
/****** Object:  StoredProcedure [dbo].[LOG_USUARIO]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LOG_USUARIO]
(@COD_USUA VARCHAR(30),
 @ALF_PASS VARCHAR(20))
AS
BEGIN
	SELECT COUNT(*)
	FROM USUARIO
	WHERE CAST(COD_USUA AS VARBINARY) = CAST(@COD_USUA AS VARBINARY)
	AND  CAST(ALF_PASS AS VARBINARY) = CAST(@ALF_PASS AS VARBINARY)
END

GO
/****** Object:  StoredProcedure [dbo].[LST_HORARIO]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LST_HORARIO]
AS
    BEGIN
        SELECT  h.COD_HORA ,
                ( h.HOR_INIC + ' - ' + h.HOR_FINA ) AS ALF_HORA ,
                h.HOR_INIC ,
                h.HOR_FINA
        FROM    HORARIO AS h
        ORDER BY ( h.HOR_INIC + ' - ' + h.HOR_FINA ) ASC
    END

GO
/****** Object:  StoredProcedure [dbo].[LST_RESERVA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LST_RESERVA]
AS
    BEGIN
        SELECT  p.COD_PEDI ,
                ISNULL(r.COD_RESE, 0) AS COD_RESE ,
                p.ALF_NUME_PEDI ,
                c.ALF_NOMB ,
                c.ALF_NUME_TELE ,
                p.FEC_HORA_RESE ,
                CONVERT(VARCHAR, p.FEC_HORA_RESE, 103) AS FEC_RESE ,
                ( h.HOR_INIC + ' - ' + h.HOR_FINA ) AS ALF_HORA ,
                td.ALF_TIPO_DEPO ,
                ( tc.ALF_TIPO_CANC + ' (' + CAST(tc.NUM_JUGA AS VARCHAR)
                  + ' jugadores)' ) AS ALF_TIPO_CANC ,
                ISNULL(r.MON_PAGA, tc.MON_PREC) AS MON_PAGA ,
                ISNULL(r.MON_PAGO, 0.00) AS MON_PAGO ,
                ( ISNULL(r.MON_PAGA, tc.MON_PREC) - ISNULL(r.MON_PAGO, 0.00) ) AS MON_DEUD ,
                CASE WHEN ISNULL(r.IND_ESTA, 'P') = 'P' THEN 'Pendiente'
                     WHEN r.IND_ESTA = 'C' THEN 'Confirmado'
                     ELSE 'Cancelado'
                END AS IND_ESTA
        FROM    PEDIDO AS p
                INNER JOIN CLIENTE AS c ON c.COD_CLIE = p.COD_CLIE
                INNER JOIN TIPO_DEPORTE AS td ON td.COD_TIPO_DEPO = p.COD_TIPO_DEPO
                INNER JOIN TIPO_CANCHA AS tc ON tc.COD_TIPO_CANC = p.COD_TIPO_CANC
                INNER JOIN HORARIO AS h ON h.COD_HORA = p.COD_HORA
                LEFT JOIN RESERVA AS r ON r.COD_PEDI = p.COD_PEDI
        WHERE   CONVERT(VARCHAR, p.FEC_HORA_RESE, 12) >= CONVERT(VARCHAR, GETDATE(), 12) AND p.IND_ESTA = 'P'
        ORDER BY p.FEC_HORA_RESE ,
                ( h.HOR_INIC + ' - ' + h.HOR_FINA )
    END


GO
/****** Object:  StoredProcedure [dbo].[LST_TIPO_CANCHA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LST_TIPO_CANCHA](@COD_TIPO_DEPO INT)
AS
BEGIN
	SELECT tc.COD_TIPO_CANC,
	       (
	           tc.ALF_TIPO_CANC + ' (' + CAST(tc.NUM_JUGA AS VARCHAR) + ' jugadores)'
	       )            AS ALF_TIPO_CANC
	FROM   TIPO_CANCHA  AS tc
	WHERE tc.COD_TIPO_DEPO = @COD_TIPO_DEPO
	ORDER BY
	       tc.ALF_TIPO_CANC ASC
END


GO
/****** Object:  StoredProcedure [dbo].[LST_TIPO_CANCHA01]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

    CREATE PROCEDURE [dbo].[LST_TIPO_CANCHA01]
    AS
        BEGIN
            SELECT  tc.COD_TIPO_CANC ,
                    tc.ALF_TIPO_CANC ,
                    tc.COD_TIPO_DEPO ,
                    tc.NUM_JUGA ,
                    tc.MON_PREC ,
                    td.ALF_TIPO_DEPO
            FROM    TIPO_CANCHA AS tc
                    INNER JOIN TIPO_DEPORTE AS td ON td.COD_TIPO_DEPO = tc.COD_TIPO_DEPO
            ORDER BY tc.ALF_TIPO_CANC
        END

GO
/****** Object:  StoredProcedure [dbo].[LST_TIPO_DEPORTE]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LST_TIPO_DEPORTE]
AS
BEGIN
	SELECT td.COD_TIPO_DEPO, 
	       td.ALF_TIPO_DEPO
	FROM TIPO_DEPORTE AS td
	ORDER BY td.ALF_TIPO_DEPO ASC
END

GO
/****** Object:  StoredProcedure [dbo].[OBT_HORARIO]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[OBT_HORARIO] ( @COD_HORA INT )
AS
    BEGIN
        SELECT  h.COD_HORA ,
                h.HOR_INIC ,
                h.HOR_FINA
        FROM    HORARIO AS h
        WHERE   h.COD_HORA = @COD_HORA
    END

GO
/****** Object:  StoredProcedure [dbo].[OBT_TIPO_CANCHA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[OBT_TIPO_CANCHA] ( @COD_TIPO_CANC INT )
AS
    BEGIN
        SELECT  tc.COD_TIPO_CANC ,
                tc.ALF_TIPO_CANC ,
                tc.COD_TIPO_DEPO ,
                tc.NUM_JUGA ,
                tc.MON_PREC
        FROM    TIPO_CANCHA AS tc
        WHERE   tc.COD_TIPO_CANC = @COD_TIPO_CANC
    END

GO
/****** Object:  StoredProcedure [dbo].[OBT_TIPO_DEPORTE]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[OBT_TIPO_DEPORTE]
    (
      @COD_TIPO_DEPO INT 
    )
AS
BEGIN
	SELECT td.COD_TIPO_DEPO, 
	       td.ALF_TIPO_DEPO
	FROM TIPO_DEPORTE AS td
	WHERE COD_TIPO_DEPO=@COD_TIPO_DEPO
END

GO
/****** Object:  StoredProcedure [dbo].[RPT_RESERVA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RPT_RESERVA]
    (
      @FEC_INIC DATETIME ,
      @FEC_FINA DATETIME
    )
AS
    BEGIN
        SELECT  p.COD_PEDI ,
                ISNULL(r.COD_RESE, 0) AS COD_RESE ,
                p.ALF_NUME_PEDI ,
                c.ALF_NOMB ,
                c.ALF_NUME_TELE ,
                p.FEC_HORA_RESE ,
                CONVERT(VARCHAR, p.FEC_HORA_RESE, 103) AS FEC_RESE ,
                ( h.HOR_INIC + ' - ' + h.HOR_FINA ) AS ALF_HORA ,
                td.ALF_TIPO_DEPO ,
                ( tc.ALF_TIPO_CANC + ' (' + CAST(tc.NUM_JUGA AS VARCHAR)
                  + ' jugadores)' ) AS ALF_TIPO_CANC ,
                ISNULL(r.MON_PAGA, tc.MON_PREC) AS MON_PAGA ,
                ISNULL(r.MON_PAGO, 0.00) AS MON_PAGO ,
                ( ISNULL(r.MON_PAGA, tc.MON_PREC) - ISNULL(r.MON_PAGO, 0.00) ) AS MON_DEUD ,
                CASE WHEN ISNULL(r.IND_ESTA, 'P') = 'P' THEN 'Pendiente'
                     WHEN r.IND_ESTA = 'C' THEN 'Confirmado'
                     ELSE 'Cancelado'
                END AS IND_ESTA
        FROM    PEDIDO AS p
                INNER JOIN CLIENTE AS c ON c.COD_CLIE = p.COD_CLIE
                INNER JOIN TIPO_DEPORTE AS td ON td.COD_TIPO_DEPO = p.COD_TIPO_DEPO
                INNER JOIN TIPO_CANCHA AS tc ON tc.COD_TIPO_CANC = p.COD_TIPO_CANC
                INNER JOIN HORARIO AS h ON h.COD_HORA = p.COD_HORA
                LEFT JOIN RESERVA AS r ON r.COD_PEDI = p.COD_PEDI
        WHERE   CONVERT(VARCHAR, p.FEC_HORA_RESE, 12) BETWEEN CONVERT(VARCHAR, @FEC_INIC, 12)
                                                      AND     CONVERT(VARCHAR, @FEC_FINA, 12)
        ORDER BY p.FEC_HORA_RESE ,
                ( h.HOR_INIC + ' - ' + h.HOR_FINA )
    END


GO
/****** Object:  StoredProcedure [dbo].[SRC_RESERVA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[SRC_RESERVA] ( @COD_PEDI INT )
AS
    BEGIN
        SELECT  r.COD_RESE ,
                p.COD_PEDI ,
                ISNULL(r.MON_PAGA, tc.MON_PREC) AS  MON_PAGA,
                ISNULL(r.MON_PAGO , 0.0) AS MON_PAGO,
                (ISNULL(r.MON_PAGA, tc.MON_PREC) - ISNULL(r.MON_PAGO , 0.0)) AS MON_DEUD,
                ISNULL(r.IND_ESTA , 'P') AS IND_ESTA
        FROM    PEDIDO AS p
                LEFT JOIN RESERVA AS r ON r.COD_PEDI = p.COD_PEDI
                LEFT JOIN TIPO_CANCHA AS tc ON tc.COD_TIPO_CANC = p.COD_TIPO_CANC
        WHERE   p.COD_PEDI = @COD_PEDI
    END

GO
/****** Object:  StoredProcedure [dbo].[UPD_RESERVA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_RESERVA]
    (
      @COD_PEDI INT ,
      @MON_PAGA NUMERIC(10, 2) ,
      @MON_PAGO NUMERIC(10, 2) ,
      @IND_CANC BIT
    )
AS
    BEGIN
        IF @IND_CANC = 1
            BEGIN            	
                UPDATE  PEDIDO
                SET     IND_ESTA = 'D'
                WHERE   COD_PEDI = @COD_PEDI
    		
                UPDATE  RESERVA
                SET     IND_ESTA = 'D'
                WHERE   COD_PEDI = @COD_PEDI
            END
        ELSE
            BEGIN
                DECLARE @SUM_PAGO INT = ISNULL(( SELECT r.MON_PAGO
                                                 FROM   RESERVA AS r
                                                 WHERE  r.COD_PEDI = @COD_PEDI
                                               ), 0.0) + @MON_PAGO
	
                IF @MON_PAGA < @SUM_PAGO
                    BEGIN
                        RAISERROR('El monto pagado supera la deuda.', 13, 1)
                        RETURN
                    END
	
                DECLARE @IND_ESTA CHAR(1)
                IF @MON_PAGA = @SUM_PAGO
                    BEGIN
                        SET @IND_ESTA = 'C'
                        UPDATE  PEDIDO
                        SET     IND_ESTA = @IND_ESTA
                        WHERE   COD_PEDI = @COD_PEDI
                    END
                ELSE
                    BEGIN
                        SET @IND_ESTA = 'P'
                    END	
		
                UPDATE  RESERVA
                SET     MON_PAGO = @SUM_PAGO ,
                        IND_ESTA = @IND_ESTA
                WHERE   COD_PEDI = @COD_PEDI
            END
    END
GO
/****** Object:  Table [dbo].[CLIENTE]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CLIENTE](
	[COD_CLIE] [int] NOT NULL,
	[ALF_NOMB] [varchar](200) NOT NULL,
	[ALF_TIPO_DOCU] [varchar](10) NOT NULL,
	[ALF_NUME_DOCU] [varchar](15) NOT NULL,
	[ALF_CORR] [varchar](50) NULL,
	[ALF_NUME_TELE] [varchar](30) NOT NULL,
 CONSTRAINT [PK_COD_CLIE] PRIMARY KEY CLUSTERED 
(
	[COD_CLIE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HORARIO]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HORARIO](
	[COD_HORA] [int] NOT NULL,
	[HOR_INIC] [char](5) NOT NULL,
	[HOR_FINA] [char](5) NOT NULL,
 CONSTRAINT [PK_COD_HORA] PRIMARY KEY CLUSTERED 
(
	[COD_HORA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PEDIDO]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PEDIDO](
	[COD_PEDI] [int] NOT NULL,
	[ALF_NUME_PEDI] [varchar](10) NOT NULL,
	[COD_CLIE] [int] NOT NULL,
	[COD_TIPO_DEPO] [int] NOT NULL,
	[COD_TIPO_CANC] [int] NOT NULL,
	[FEC_HORA_RESE] [smalldatetime] NOT NULL,
	[COD_HORA] [int] NULL,
	[IND_ESTA] [char](1) NOT NULL,
 CONSTRAINT [PK_COD_PEDI] PRIMARY KEY CLUSTERED 
(
	[COD_PEDI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RESERVA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RESERVA](
	[COD_RESE] [int] NOT NULL,
	[COD_PEDI] [int] NOT NULL,
	[MON_PAGA] [numeric](10, 2) NOT NULL,
	[MON_PAGO] [numeric](10, 2) NOT NULL,
	[IND_ESTA] [char](1) NOT NULL,
 CONSTRAINT [PK_COD_RESE] PRIMARY KEY CLUSTERED 
(
	[COD_RESE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TIPO_CANCHA]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TIPO_CANCHA](
	[COD_TIPO_CANC] [int] NOT NULL,
	[ALF_TIPO_CANC] [varchar](100) NOT NULL,
	[COD_TIPO_DEPO] [int] NOT NULL,
	[NUM_JUGA] [int] NOT NULL,
	[MON_PREC] [numeric](10, 2) NULL,
 CONSTRAINT [PK_TIPO_CANCHA] PRIMARY KEY CLUSTERED 
(
	[COD_TIPO_CANC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TIPO_DEPORTE]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TIPO_DEPORTE](
	[COD_TIPO_DEPO] [int] NOT NULL,
	[ALF_TIPO_DEPO] [varchar](100) NOT NULL,
 CONSTRAINT [PK_TIPO_DEPORTE] PRIMARY KEY CLUSTERED 
(
	[COD_TIPO_DEPO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 4/12/2016 03:56:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[USUARIO](
	[COD_USUA] [varchar](30) NULL,
	[ALF_PASS] [varchar](20) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[CLIENTE] ([COD_CLIE], [ALF_NOMB], [ALF_TIPO_DOCU], [ALF_NUME_DOCU], [ALF_CORR], [ALF_NUME_TELE]) VALUES (1, N'Manuel Puelles', N'DNI', N'42579246', N'netsshey@gmail.com', N'994545619')
INSERT [dbo].[CLIENTE] ([COD_CLIE], [ALF_NOMB], [ALF_TIPO_DOCU], [ALF_NUME_DOCU], [ALF_CORR], [ALF_NUME_TELE]) VALUES (2, N'Ivan Puelles', N'DNI', N'63589247', N'ipuelles@hotmail.com', N'998745612')
INSERT [dbo].[CLIENTE] ([COD_CLIE], [ALF_NOMB], [ALF_TIPO_DOCU], [ALF_NUME_DOCU], [ALF_CORR], [ALF_NUME_TELE]) VALUES (3, N'Luis Sanchez', N'DNI', N'65656576', N'lsanchez@gmail.com', N'987654321')
INSERT [dbo].[CLIENTE] ([COD_CLIE], [ALF_NOMB], [ALF_TIPO_DOCU], [ALF_NUME_DOCU], [ALF_CORR], [ALF_NUME_TELE]) VALUES (4, N'Jose', N'DNI', N'343242', N'net@gmail.com', N'63635353')
INSERT [dbo].[CLIENTE] ([COD_CLIE], [ALF_NOMB], [ALF_TIPO_DOCU], [ALF_NUME_DOCU], [ALF_CORR], [ALF_NUME_TELE]) VALUES (5, N'Luis Puelles', N'DNI', N'42579287', N'netsshey@gmail.com', N'994545619')
INSERT [dbo].[CLIENTE] ([COD_CLIE], [ALF_NOMB], [ALF_TIPO_DOCU], [ALF_NUME_DOCU], [ALF_CORR], [ALF_NUME_TELE]) VALUES (6, N'Marco Peña', N'DNI', N'64758690', N'mpna@outlook.com', N'987654321')
INSERT [dbo].[CLIENTE] ([COD_CLIE], [ALF_NOMB], [ALF_TIPO_DOCU], [ALF_NUME_DOCU], [ALF_CORR], [ALF_NUME_TELE]) VALUES (7, N'Luis Perez', N'DNI', N'98765432', N'lperez@gmail.com', N'987654321')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (1, N'10:00', N'11:00')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (2, N'11:00', N'12:00')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (3, N'12:00', N'13:00')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (4, N'14:00', N'15:00')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (5, N'15:00', N'16:00')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (6, N'16:00', N'17:00')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (7, N'17:00', N'18:00')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (8, N'18:00', N'19:00')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (9, N'19:00', N'20:00')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (10, N'20:00', N'21:00')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (11, N'21:00', N'22:00')
INSERT [dbo].[HORARIO] ([COD_HORA], [HOR_INIC], [HOR_FINA]) VALUES (12, N'22:00', N'23:00')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (1, N'P0001', 1, 1, 2, CAST(0xA6BB0000 AS SmallDateTime), 2, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (2, N'P0002', 1, 1, 2, CAST(0xA6BB0000 AS SmallDateTime), 4, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (3, N'P0003', 1, 3, 10, CAST(0xA6BB0000 AS SmallDateTime), 7, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (4, N'P0004', 1, 1, 2, CAST(0xA6BB0000 AS SmallDateTime), 8, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (5, N'P0005', 1, 1, 2, CAST(0xA6BB0000 AS SmallDateTime), 5, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (6, N'P0006', 2, 2, 7, CAST(0xA6BB0000 AS SmallDateTime), 6, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (7, N'P0007', 3, 2, 7, CAST(0xA6BC0000 AS SmallDateTime), 1, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (8, N'P0008', 1, 4, 11, CAST(0xA6BC0000 AS SmallDateTime), 9, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (9, N'P0009', 1, 1, 3, CAST(0xA6BC0000 AS SmallDateTime), 10, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (10, N'P0010', 4, 1, 3, CAST(0xA6BC0000 AS SmallDateTime), 10, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (11, N'P0011', 1, 1, 2, CAST(0xA6C70000 AS SmallDateTime), 8, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (12, N'P0012', 5, 1, 2, CAST(0xA6CC0000 AS SmallDateTime), 8, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (13, N'P0013', 6, 3, 10, CAST(0xA6C80000 AS SmallDateTime), 5, N'P')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (14, N'P0014', 1, 1, 2, CAST(0xA6D20000 AS SmallDateTime), 7, N'D')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (15, N'P0015', 7, 3, 10, CAST(0xA6D20000 AS SmallDateTime), 9, N'D')
INSERT [dbo].[PEDIDO] ([COD_PEDI], [ALF_NUME_PEDI], [COD_CLIE], [COD_TIPO_DEPO], [COD_TIPO_CANC], [FEC_HORA_RESE], [COD_HORA], [IND_ESTA]) VALUES (16, N'P0016', 1, 1, 2, CAST(0xA6D30000 AS SmallDateTime), 9, N'P')
INSERT [dbo].[RESERVA] ([COD_RESE], [COD_PEDI], [MON_PAGA], [MON_PAGO], [IND_ESTA]) VALUES (1, 12, CAST(100.00 AS Numeric(10, 2)), CAST(90.00 AS Numeric(10, 2)), N'P')
INSERT [dbo].[RESERVA] ([COD_RESE], [COD_PEDI], [MON_PAGA], [MON_PAGO], [IND_ESTA]) VALUES (2, 15, CAST(80.00 AS Numeric(10, 2)), CAST(40.00 AS Numeric(10, 2)), N'D')
INSERT [dbo].[RESERVA] ([COD_RESE], [COD_PEDI], [MON_PAGA], [MON_PAGO], [IND_ESTA]) VALUES (3, 14, CAST(100.00 AS Numeric(10, 2)), CAST(60.00 AS Numeric(10, 2)), N'D')
INSERT [dbo].[RESERVA] ([COD_RESE], [COD_PEDI], [MON_PAGA], [MON_PAGO], [IND_ESTA]) VALUES (4, 16, CAST(100.00 AS Numeric(10, 2)), CAST(60.00 AS Numeric(10, 2)), N'P')
INSERT [dbo].[TIPO_CANCHA] ([COD_TIPO_CANC], [ALF_TIPO_CANC], [COD_TIPO_DEPO], [NUM_JUGA], [MON_PREC]) VALUES (1, N'Césped Natural', 1, 6, CAST(80.00 AS Numeric(10, 2)))
INSERT [dbo].[TIPO_CANCHA] ([COD_TIPO_CANC], [ALF_TIPO_CANC], [COD_TIPO_DEPO], [NUM_JUGA], [MON_PREC]) VALUES (2, N'Césped Natural', 1, 7, CAST(100.00 AS Numeric(10, 2)))
INSERT [dbo].[TIPO_CANCHA] ([COD_TIPO_CANC], [ALF_TIPO_CANC], [COD_TIPO_DEPO], [NUM_JUGA], [MON_PREC]) VALUES (3, N'Césped Sintético', 1, 6, CAST(60.00 AS Numeric(10, 2)))
INSERT [dbo].[TIPO_CANCHA] ([COD_TIPO_CANC], [ALF_TIPO_CANC], [COD_TIPO_DEPO], [NUM_JUGA], [MON_PREC]) VALUES (4, N'Césped Sintético', 1, 7, CAST(50.00 AS Numeric(10, 2)))
INSERT [dbo].[TIPO_CANCHA] ([COD_TIPO_CANC], [ALF_TIPO_CANC], [COD_TIPO_DEPO], [NUM_JUGA], [MON_PREC]) VALUES (5, N'Madera dura ', 2, 7, CAST(90.00 AS Numeric(10, 2)))
INSERT [dbo].[TIPO_CANCHA] ([COD_TIPO_CANC], [ALF_TIPO_CANC], [COD_TIPO_DEPO], [NUM_JUGA], [MON_PREC]) VALUES (7, N'Asfalto', 2, 7, CAST(70.00 AS Numeric(10, 2)))
INSERT [dbo].[TIPO_CANCHA] ([COD_TIPO_CANC], [ALF_TIPO_CANC], [COD_TIPO_DEPO], [NUM_JUGA], [MON_PREC]) VALUES (8, N'Césped', 3, 4, CAST(130.00 AS Numeric(10, 2)))
INSERT [dbo].[TIPO_CANCHA] ([COD_TIPO_CANC], [ALF_TIPO_CANC], [COD_TIPO_DEPO], [NUM_JUGA], [MON_PREC]) VALUES (9, N'Arcilla', 3, 4, CAST(150.00 AS Numeric(10, 2)))
INSERT [dbo].[TIPO_CANCHA] ([COD_TIPO_CANC], [ALF_TIPO_CANC], [COD_TIPO_DEPO], [NUM_JUGA], [MON_PREC]) VALUES (10, N'Asfalto', 3, 4, CAST(80.00 AS Numeric(10, 2)))
INSERT [dbo].[TIPO_CANCHA] ([COD_TIPO_CANC], [ALF_TIPO_CANC], [COD_TIPO_DEPO], [NUM_JUGA], [MON_PREC]) VALUES (11, N'Asfalto', 4, 6, CAST(90.00 AS Numeric(10, 2)))
INSERT [dbo].[TIPO_DEPORTE] ([COD_TIPO_DEPO], [ALF_TIPO_DEPO]) VALUES (1, N'Fútbol')
INSERT [dbo].[TIPO_DEPORTE] ([COD_TIPO_DEPO], [ALF_TIPO_DEPO]) VALUES (2, N'Baloncesto')
INSERT [dbo].[TIPO_DEPORTE] ([COD_TIPO_DEPO], [ALF_TIPO_DEPO]) VALUES (3, N'Tenis')
INSERT [dbo].[TIPO_DEPORTE] ([COD_TIPO_DEPO], [ALF_TIPO_DEPO]) VALUES (4, N'Voleibol')
INSERT [dbo].[USUARIO] ([COD_USUA], [ALF_PASS]) VALUES (N'admin', N'admin')
ALTER TABLE [dbo].[PEDIDO]  WITH CHECK ADD  CONSTRAINT [FK_PEDIDO_HORARIO] FOREIGN KEY([COD_HORA])
REFERENCES [dbo].[HORARIO] ([COD_HORA])
GO
ALTER TABLE [dbo].[PEDIDO] CHECK CONSTRAINT [FK_PEDIDO_HORARIO]
GO
ALTER TABLE [dbo].[PEDIDO]  WITH CHECK ADD  CONSTRAINT [FK_PEDIDO_TIPO_CANCHA] FOREIGN KEY([COD_TIPO_CANC])
REFERENCES [dbo].[TIPO_CANCHA] ([COD_TIPO_CANC])
GO
ALTER TABLE [dbo].[PEDIDO] CHECK CONSTRAINT [FK_PEDIDO_TIPO_CANCHA]
GO
ALTER TABLE [dbo].[PEDIDO]  WITH CHECK ADD  CONSTRAINT [FK_PEDIDO_TIPO_DEPORTE] FOREIGN KEY([COD_TIPO_DEPO])
REFERENCES [dbo].[TIPO_DEPORTE] ([COD_TIPO_DEPO])
GO
ALTER TABLE [dbo].[PEDIDO] CHECK CONSTRAINT [FK_PEDIDO_TIPO_DEPORTE]
GO
ALTER TABLE [dbo].[TIPO_CANCHA]  WITH CHECK ADD  CONSTRAINT [FK_TIPO_CANCHA_TIPO_DEPORTE] FOREIGN KEY([COD_TIPO_DEPO])
REFERENCES [dbo].[TIPO_DEPORTE] ([COD_TIPO_DEPO])
GO
ALTER TABLE [dbo].[TIPO_CANCHA] CHECK CONSTRAINT [FK_TIPO_CANCHA_TIPO_DEPORTE]
GO
