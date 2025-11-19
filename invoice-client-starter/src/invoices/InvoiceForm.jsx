import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { apiGet, apiPost, apiPut } from "../utils/api";

import InputField from "../components/InputField";
import FlashMessage from "../components/FlashMessage";

const InvoiceForm = () => {
  const navigate = useNavigate();
  const { id } = useParams();

const [invoice, setInvoice] = useState({
  invoiceNumber: "",
  issued: "",
  dueDate: "",
  product: "",
  price: "",
  vat: 21,
  sellerId: "",
  buyerId: "",
  note: "",
});

  const [sentState, setSent] = useState(false);
  const [successState, setSuccess] = useState(false);
  const [errorState, setError] = useState(null);

useEffect(() => {
  if (id) {
    apiGet("/api/invoices/" + id)
      .then((data) =>
        setInvoice({
          invoiceNumber: data.invoiceNumber ?? "",
          issued: data.issued ?? "",
          dueDate: data.dueDate ?? "",
          product: data.product ?? "",
          price: data.price ?? "",
          vat: data.vat ?? 21,
          sellerId: data.seller?._id ?? "",
          buyerId: data.buyer?._id ?? "",
          note: data.note ?? "",
        })
      )
      .catch((err) => console.error("Chyba při načítání faktury:", err));
  }
}, [id]);

 const handleSubmit = (e) => {
  e.preventDefault();

  const payload = {
    invoiceNumber: Number(invoice.invoiceNumber),
    issued: invoice.issued,   // "2025-10-20" из <input type="date">
    dueDate: invoice.dueDate,
    product: invoice.product,
    price: Number(invoice.price),
    vat: Number(invoice.vat),
    note: invoice.note,
    seller: invoice.sellerId
      ? { _id: Number(invoice.sellerId) }
      : null,
    buyer: invoice.buyerId
      ? { _id: Number(invoice.buyerId) }
      : null,
  };

  const request = id
    ? apiPut("/api/invoices/" + id, payload)
    : apiPost("/api/invoices", payload);

  request
    .then(() => {
      setSent(true);
      setSuccess(true);
      navigate("/invoices");
    })
    .catch((error) => {
      console.log(error.message);
      setError(error.message);
      setSent(true);
      setSuccess(false);
    });
};

  const sent = sentState;
  const success = successState;

  return (
    <div>
      <h1>{id ? "Upravit fakturu" : "Vytvořit fakturu"}</h1>
      <hr />
      {errorState && <div className="alert alert-danger">{errorState}</div>}
      {sent && (
        <FlashMessage
          theme={success ? "success" : ""}
          text={success ? "Faktura byla úspěšně uložena." : ""}
        />
      )}

      <form onSubmit={handleSubmit}>
        <InputField
  required
  type="text"
  name="invoiceNumber"
  label="Číslo faktury"
  value={invoice.invoiceNumber}
  handleChange={(e) =>
    setInvoice({ ...invoice, invoiceNumber: e.target.value })
  }
/>

<InputField
  required
  type="date"
  name="issued"
  label="Datum vystavení"
  value={invoice.issued}
  handleChange={(e) =>
    setInvoice({ ...invoice, issued: e.target.value })
  }
/>

<InputField
  required
  type="date"
  name="dueDate"
  label="Datum splatnosti"
  value={invoice.dueDate}
  handleChange={(e) =>
    setInvoice({ ...invoice, dueDate: e.target.value })
  }
/>

<InputField
  required
  type="text"
  name="product"
  label="Produkt / služba"
  value={invoice.product}
  handleChange={(e) =>
    setInvoice({ ...invoice, product: e.target.value })
  }
/>

<InputField
  required
  type="number"
  name="price"
  label="Cena bez DPH (Kč)"
  value={invoice.price}
  handleChange={(e) =>
    setInvoice({ ...invoice, price: e.target.value })
  }
/>

<InputField
  required
  type="number"
  name="vat"
  label="DPH (%)"
  value={invoice.vat}
  handleChange={(e) =>
    setInvoice({ ...invoice, vat: e.target.value })
  }
/>
<InputField
  required
  type="number"
  name="sellerId"
  label="Dodavatel (ID osoby)"
  value={invoice.sellerId}
  handleChange={(e) =>
    setInvoice({ ...invoice, sellerId: e.target.value })
  }
/>

<InputField
  required
  type="number"
  name="buyerId"
  label="Odběratel (ID osoby)"
  value={invoice.buyerId}
  handleChange={(e) =>
    setInvoice({ ...invoice, buyerId: e.target.value })
  }
/>
<InputField
  type="text"
  name="note"
  label="Poznámka"
  value={invoice.note}
  handleChange={(e) =>
    setInvoice({ ...invoice, note: e.target.value })
  }
/>


        <input type="submit" className="btn btn-primary" value="Uložit" />
      </form>
    </div>
  );
};

export default InvoiceForm;