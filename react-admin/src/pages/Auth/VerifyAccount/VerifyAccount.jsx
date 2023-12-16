import styles from "./VerifyAccount.module.scss";
import { useParams } from "react-router-dom";
import React from "react";
import {
  useTheme,
  TextField,
  Card,
  Button,
  Grid,
  Box,
  Typography,
} from "@mui/material";
import { Form, Formik, useField, Field } from "formik";
import { ReactComponent as Logo } from "../../../assets/icons/lock-screen.svg";
import { VerifyAccountSchema } from "../../../validations/FormSchemas";
import authRoutes from "../../../routing/authRoutes";
import adminRoutes from "../../../routing/adminRoutes";
import axios from "axios";
// import {
//   ACCOUNT_VERIFYACCOUNT_URL,
//   ACCOUNT_RESEND_OTP_URL,
// } from "@/constants/Urls";
import useService from "../../../hooks";
import { useNavigate } from "react-router-dom";
import { useSnackbar } from "notistack";
import { findPathByLabel } from "../../../utils/routingUtils";

const AppTextField = (props) => {
  const [field, meta] = useField(props);
  const errorText = meta.error && meta.touched ? meta.error : " ";
  return (
    <TextField
      {...props}
      {...field}
      helperText={errorText}
      error={meta.touched && !!meta.error}
      sx={{
        width: "100%",
        mb: 1,
      }}
    />
  );
};

const initialValues = {
  otp: "",
};

function VerifyAccount() {
  const { enqueueSnackbar } = useSnackbar();
  const navigate = useNavigate();
  const theme = useTheme();
  const [serverErrors, setServerErrors] = React.useState({});
  const { userId } = useParams();
  const { userServices } = useService();

  const handleFormSubmit = async (values, { setSubmitting, setErrors }) => {
    setSubmitting(true);

    const data = {
      otp: values.otp,
    };

    try {
      // const response = await userServices.verifyAccount(userId, { otp: values.otp });
      const response = await userServices.verifyAccountRequest(userId, data);
      console.log("Verification successful:", response.data);
      enqueueSnackbar("Account verified successfully!", {
        variant: "success",
        autoHideDuration: 5000,
      });
      navigate(findPathByLabel(authRoutes, "SignIn"));
    } catch (error) {
      let errorMessage = "An error occurred during verification.";
      if (error.response) {
        if (error.response.status === 500) {
          navigate("/error500");
          return;
        } else if (error.response.data && error.response.data.errors) {
          const errorsFromServer = error.response.data.errors;
          setErrors(errorsFromServer);
          setServerErrors(errorsFromServer); // Store the server errors
          console.log(errorsFromServer);
          errorMessage = "Please correct the highlighted errors.";
        }
      }
      enqueueSnackbar(errorMessage, {
        variant: "error",
      });
    }finally {
      setSubmitting(false);
    }

  };

  const handleResendOTP = async () => {
    try {
      console.log("Resend OTP button clicked");
      const response = await userServices.resendOTPRequest(userId);
      console.log("Resend OTP successful:", response.data);
      enqueueSnackbar("OTP resent successfully!", {
        variant: "success",
        autoHideDuration: 5000,
      });
    } catch (error) {
      let errorMessage = "An error occurred while resending the OTP.";
      if (error.response && error.response.status === 500) {
        navigate("/error500");
        return;
      } else if (error.response && error.response.data) {
        errorMessage = error.response.data.message || errorMessage;
      }
      enqueueSnackbar(errorMessage, {
        variant: "error",
      });
    }
  };

  return (
    <Box
      sx={{
        padding: "1rem",
        display: "flex",
        minHeight: "100vh",
        flex: 1,
        width: "100%",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
      }}
    >
      <Card
        sx={{
          maxWidth: 1024,
          borderRadius: "2%",
          width: "100%",
          backgroundColor: "#1F2A40",
          paddingX: 8,
          paddingY: 3,
          paddingLeft: { xs: 8, md: 2 },
          overflow: "hidden",
          boxShadow:
            "0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)",
        }}
      >
        <Grid
          container
          spacing={5}
          sx={{
            alignItems: { md: "center" },
          }}
        >
          <Grid
            item
            xs={12}
            md={6}
            sx={{
              textAlign: "center",
            }}
          >
            <Logo fill={theme.palette.primary.main} />
          </Grid>
          <Grid
            item
            xs={12}
            md={6}
            sx={{
              textAlign: "center",
            }}
          >
            <Box
              sx={{
                mb: { xs: 6, xl: 3 },
                fontWeight: 700,
                fontSize: 30,
              }}
            >
              Verify Account
            </Box>
            {Object.keys(serverErrors).length > 0 && (
              <Box
                bgcolor="error.main"
                color="white"
                p={2}
                borderRadius={2}
                sx={{
                  mb: { xs: 6, xl: 3 },
                }}
              >
                <Typography variant="h5">Errors:</Typography>
                <ul>
                  {Object.entries(serverErrors).map(([field, errors]) => (
                    <li key={field}>
                      {errors.map((error, index) => (
                        <div key={index}>{error}</div>
                      ))}
                    </li>
                  ))}
                </ul>
              </Box>
            )}
            <Formik
              validateOnChange={true}
              initialValues={initialValues}
              // validationSchema={VerifyAccountSchema}
              // validator={() => ({})}
              onSubmit={handleFormSubmit}
            >
              {({ isSubmitting, values, handleChange }) => (
                <Form noValidate autoComplete="on">
                  <Grid item xs={12}>
                    <AppTextField
                      label="Otp"
                      name="otp"
                      variant="outlined"
                      autoComplete="otp"
                    />
                  </Grid>
                  <Button
                    variant="contained"
                    color="primary"
                    disabled={isSubmitting}
                    sx={{
                      width: "100%",
                      height: 44,
                      marginBottom: 2,
                    }}
                    type="submit"
                  >
                    Verify
                  </Button>
                </Form>
              )}
            </Formik>

            <Box
              sx={{
                fontSize: 14,
                fontWeight: 700,
              }}
            >
              <Box component="span" sx={{ mr: 2 }}>
                Has the one-time password still not reached you?
              </Box>
              <Box
                component="span"
                sx={{
                  color: "primary.main",
                  cursor: "pointer",
                }}
                onClick={handleResendOTP}
              >
                Resend OTP
              </Box>
            </Box>
          </Grid>
        </Grid>
      </Card>
    </Box>
  );
}

export default VerifyAccount;
