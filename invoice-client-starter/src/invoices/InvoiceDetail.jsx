import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { apiGet } from "../utils/api";

const InvoiceDetail = () => {
  const { id } = useParams();
  const [invoice, setInvoice] = useState({});

  useEffect(() => {
    apiGet("/api/invoices/" + id)
      .then((data) => setInvoice(data))
      .catch((err) => console.error("Chyba při načítání faktury:", err));
  }, [id]);

  // vypočítat cenu s DPH (pokud ještě není v datech)
  const priceWithVat = invoice.price
    ? (invoice.price * (1 + invoice.vat / 100)).toFixed(2)
    : "";

  return (
    <div>
      <h1>Detail faktury</h1>
      <hr />

      <h3>
        Faktura č. {invoice.invoiceNumber}{" "}
        {invoice.issued && (
          <small className="text-muted">
            (vystavena: {new Date(invoice.issued).toLocaleDateString("cs-CZ")})
          </small>
        )}
      </h3>

      <p>
        <strong>Dodavatel:</strong>
        <br />
        {invoice.seller?.name}
      </p>

      <p>
        <strong>Odběratel:</strong>
        <br />
        {invoice.buyer?.name}
      </p>

      <p>
        <strong>Produkt:</strong>
        <br />
        {invoice.product}
      </p>

      <p>
        <strong>Cena bez DPH:</strong>
        <br />
        {invoice.price} Kč
      </p>

      <p>
        <strong>DPH:</strong>
        <br />
        {invoice.vat} %
      </p>

      <p>
        <strong>Cena s DPH:</strong>
        <br />
        {priceWithVat} Kč
      </p>

      <p>
        <strong>Poznámka:</strong>
        <br />
        {invoice.note}
      </p>
    </div>
  );
};

export default InvoiceDetail;