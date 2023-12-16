import styles from "./ResetPasswordMessage.module.scss";
import { Box, Card, Grid, Typography, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";
import authRoutes from "../../../routing/authRoutes";
import { findPathByLabel } from "../../../utils/routingUtils";
import { useTheme } from "@mui/material";
import SuccessImage from '../../../assets/icons/reset-password-message.svg';

function ResetPasswordMessage() {
  const navigate = useNavigate();
  const theme = useTheme();

  return (
    <Box
      sx={{
        padding: "1rem",
        display: "flex",
        minHeight: "100vh",
        flex: 1,
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
          boxShadow:
            "0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)",
        }}
      >
        <Grid
          container
          spacing={5}
          sx={{
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <Grid item xs={12} md={12}>
            <Box sx={{ textAlign: "center" }}>
              <img src={SuccessImage} alt="Success" style={{ maxWidth: '350px', marginBottom: '10px' }} />
            </Box>
            <Typography
              variant="h3"
              sx={{
                textAlign: "center",
                mb: { xs: 6, xl: 3 },
                fontWeight: 700,
                fontSize: 30,
                color: theme.palette.primary.main,
              }}
            >
              Success!
            </Typography>
            <Typography
              variant="body1"
              sx={{
                textAlign: "center",
                mb: 3,
                color: "white",
              }}
            >
              Instructions on how to reset your password have just been sent to
              your email!
            </Typography>
            <Box sx={{ textAlign: "center", mt: 3 }}>
              <Button
                variant="contained"
                color="primary"
                onClick={() => navigate(findPathByLabel(authRoutes, "SignIn"))}
                sx={{
                  width: "100%",
                  maxWidth: "200px",
                  height: 44,
                }}
              >
                Back to Sign In
              </Button>
            </Box>
          </Grid>
        </Grid>
      </Card>
    </Box>
  );
}

export default ResetPasswordMessage;
