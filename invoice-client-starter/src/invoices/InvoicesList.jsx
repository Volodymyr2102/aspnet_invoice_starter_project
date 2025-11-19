import { useEffect, useState } from "react";
import axios from "axios";

export default function InvoicesList() {
  const [invoices, setInvoices] = useState([]);
  const API_URL = "https://localhost:7071/api/invoices";

  useEffect(() => {
    axios.get(API_URL)
      .then(res => setInvoices(res.data))
      .catch(err => console.error("Chyba při načítání faktur:", err));
  }, []);

  return (
    <div>
      <h2>Seznam faktur</h2>
      <table border="1" cellPadding="5">
        <thead>
          <tr>
            <th>ID</th>
            <th>Produkt</th>
            <th>Cena</th>
            <th>DPH</th>
            <th>Akce</th>
          </tr>
        </thead>
        <tbody>
          {invoices.map(inv => (
            <tr key={inv.invoiceId}>
              <td>{inv.invoiceId}</td>
              <td>{inv.product}</td>
              <td>{inv.price}</td>
              <td>{inv.vat}%</td>
              <td>
                <button>Detail</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}