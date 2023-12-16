import React from "react";
import {
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Button,
  Typography,
  useTheme,
} from "@mui/material";
import { tokens } from "../../../layouts/Admin/AdminTheme";
import styles from "./DeleteModal.module.scss";

function DeleteModal({ isOpen, onClose, onDelete }) {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  return (
    <Dialog
      open={isOpen}
      onClose={onClose}
      PaperProps={{
        style: {
          backgroundColor: colors.blueAccent[700],
        },
      }}
    >
      <DialogTitle>
          Confirm Deletion
      </DialogTitle>
      <DialogContent>
        <DialogContentText>
            Are you sure you want to delete this item? This action cannot be
            undone.
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button
          onClick={onClose}
          style={{
            color: colors.grey[600],
            fontWeight: "bold",
            fontSize: "0.8rem",
          }}
        >
          Cancel
        </Button>
        <Button onClick={onDelete} style={{
            color: '#880808',
            fontWeight: "bold",
            fontSize: "0.8rem",
          }} autoFocus>
          Delete
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default DeleteModal;